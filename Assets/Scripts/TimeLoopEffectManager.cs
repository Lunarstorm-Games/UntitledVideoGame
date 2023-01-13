using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Renderpasses;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    public class TimeLoopEffectManager : MonoBehaviour
    {
        public bool DeleteOnStart = false;

        [ReadOnly] public Texture2D lastScreenshot;
        public Camera recordingCamera;
        public int RecordingFrameRate = 15;
        public RenderTexture RT_TimeLoop;
        public float slowDownTime = 3f;
        public int MaxScreenshots = 10000;
        public bool ItsRewindTime = true;
        public float TargetRewindDuration = 10f;
        public Material blitMaterial;
        private readonly List<Texture2D> capturedFrames = new();
        private ScriptableRendererData _scriptableRendererData;
        private AudioSource audioSource;
        private Blit blitRenderer;
        private bool CaptureStarted;
        private string Folder;
        private float frameTime;
        private float replayTimeBetweenFrames = 0.03333333333f;

        private void Awake()

        {
            Folder = Application.temporaryCachePath + "/timeloopEffect";
            audioSource = GetComponent<AudioSource>();
            ResizeRenderTexture(RT_TimeLoop,Screen.width,Screen.height);
        }


        private void Start()
        {
            ExtractScriptableRendererData();
            StartCapture();
            blitRenderer =
                _scriptableRendererData.rendererFeatures.FirstOrDefault(x => x.GetType() == typeof(Blit)) as Blit;
            blitRenderer.SetActive(false);

            if (DeleteOnStart) Directory.Delete(Folder);
            Directory.CreateDirectory(Folder);
            //Camera.onPostRender += SaveCameraView;
            //RenderPipelineManager.endFrameRendering += SaveCameraView;
        }

        private void Update()
        {
            if (CaptureStarted)
            {
                var targetDelta = 1f / RecordingFrameRate;
                if (frameTime > targetDelta)
                {
                    //capturedFrames.Add(ScreenCapture.CaptureScreenshotAsTexture());
                    //capturedFrames.Add(SaveCameraView(Camera.main));
                    frameTime = 0;
                    RenderedTextureFromCamera();
                    
                }

                frameTime += Time.deltaTime;
            }
        }

        public void StartCapture()
        {
            CaptureStarted = true;
        }

        public void StartTimeloopEffect(Action afterCompleteCallback)
        {
            if(ItsRewindTime) audioSource.Play();
            Time.timeScale = 0;
            UIController.Instance.gameObject.SetActive(false);

            blitRenderer.SetActive(true);
            CalculateFrameDelta();
            StartCoroutine(PlaybackCoroutine(afterCompleteCallback));
        }

        void ResizeRenderTexture(RenderTexture renderTexture, int width, int height)
        {
            if (renderTexture)
            {
                renderTexture.Release();
                renderTexture.width = width;
                renderTexture.height = height;
            }

            recordingCamera.targetTexture = renderTexture;

        }

        private void CalculateFrameDelta()
        {
            var delta = TargetRewindDuration / capturedFrames.Count;
            if(delta< 0.03333333333f) replayTimeBetweenFrames = delta;

        }

        public void StopCapture()
        {
            CaptureStarted = false;
        }

        private void ExtractScriptableRendererData()
        {
            var pipeline = (UniversalRenderPipelineAsset) GraphicsSettings.currentRenderPipeline;
            var propertyInfo = pipeline.GetType()
                .GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
            _scriptableRendererData = ((ScriptableRendererData[]) propertyInfo?.GetValue(pipeline))?[0];

            //foreach (var renderObjSetting in _scriptableRendererData.rendererFeatures.OfType<UnityEngine.Experimental.Rendering.Universal.RenderObjects>())
            //{
            //    renderObjSetting.settings.cameraSettings.cameraFieldOfView = _currentFPSFov;
            //    renderObjSetting.settings.cameraSettings.offset = _currentFPSOffset;
            //}
        }


        private IEnumerator PlaybackCoroutine(Action afterCompleteCallback)
        {
            for (var i = capturedFrames.Count - 1; i >= 0; i--)
            {
                var texture = capturedFrames[i];
                try
                {
                    //uiImage.texture = texture;
                    blitRenderer.settings.blitMaterial.mainTexture = texture;
                }

                catch (Exception e)
                {
                    Debug.LogError(e);
                }

                yield return new WaitForSecondsRealtime(replayTimeBetweenFrames);
            }

            UIController.Instance.gameObject.SetActive(true);
            afterCompleteCallback();
        }

        private void RenderedTextureFromCamera()
        {
            
            //RenderTexture.active = RT_TimeLoop;

            Texture2D renderedTexture = null;


            AsyncGPUReadback.Request(RT_TimeLoop, 0, TextureFormat.RGBA32,(AsyncGPUReadbackRequest request) =>
            {

                OnCompleteReadback(request,out renderedTexture);

                if (capturedFrames.Count >= MaxScreenshots)
                {

                    capturedFrames.RemoveAt(0);
                }

                capturedFrames.Add(renderedTexture);
                lastScreenshot = renderedTexture;
                blitMaterial.SetTexture("_BaseMap",renderedTexture);
            });
            //renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            //renderedTexture.Apply();

            //RenderTexture.active = null;
        }

        private void OnCompleteReadback(AsyncGPUReadbackRequest asyncGPUReadbackRequest,out  Texture2D texture)
        {
            texture = null;
            if (asyncGPUReadbackRequest.hasError)
            {
                Debug.LogError("Error Capturing Screenshot: With AsyncGPUReadbackRequest.");
                return;
            }
            var rawData = asyncGPUReadbackRequest.GetData<byte>();
            // Grab screen dimensions
            var width = RT_TimeLoop.width;
            var height = RT_TimeLoop.height;
            texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.SetPixelData(rawData,0);
            texture.Apply();
            
        }
    }
}