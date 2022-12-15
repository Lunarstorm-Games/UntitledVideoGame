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
        private readonly List<Texture2D> capturedFrames = new();
        private ScriptableRendererData _scriptableRendererData;
        private AudioSource audioSource;
        private Blit blitRenderer;
        private bool CaptureStarted;
        public bool DeleteOnStart = false;
        private string Folder;
        private float frameTime;

        [ReadOnly] public Texture2D lastScreenshot;
        public Camera recordingCamera;
        public int RecordingFrameRate = 15;
        public float replayTimeBetweenFrames = 0.1f;
        public RenderTexture RT_TimeLoop;
        public float slowDownTime = 3f;
        public int MaxScreenshots = 10000;
        public bool ItsRewindTime = true;

        public void StartCapture()
        {
            CaptureStarted = true;
        }

        public void StartTimeloopEffect(Action afterCompleteCallback)
        {
            Time.timeScale = 0;
            UIController.Instance.gameObject.SetActive(false);

            blitRenderer.SetActive(true);
            if(ItsRewindTime) audioSource.Play();

            StartCoroutine(PlaybackCoroutine(afterCompleteCallback));
        }

        public void StopCapture()
        {
            CaptureStarted = false;
        }

        private void Awake()

        {
            Folder = Application.temporaryCachePath + "/timeloopEffect";
            audioSource = GetComponent<AudioSource>();
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

        private Texture2D RenderedTextureFromCamera()
        {
            var screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
            recordingCamera.targetTexture = screenTexture;
            RenderTexture.active = screenTexture;
            recordingCamera.Render();

            var renderedTexture = new Texture2D(Screen.width, Screen.height);
            renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            renderedTexture.Apply();

            RenderTexture.active = null;

            lastScreenshot = renderedTexture;
            return renderedTexture;
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
                    var renderedTexture = RenderedTextureFromCamera();
                    if (capturedFrames.Count >= MaxScreenshots)
                    {

                        capturedFrames.RemoveAt(0);
                    }

                    capturedFrames.Add(renderedTexture);
                }

                frameTime += Time.deltaTime;
            }
        }
    }
}