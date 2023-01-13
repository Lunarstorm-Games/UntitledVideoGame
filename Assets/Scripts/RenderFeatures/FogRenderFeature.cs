using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FogRenderFeature : ScriptableRendererFeature
{
    private FogPass _FogPass;
    public Material material;
    public Mesh mesh;
    public bool inEditor = false;
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (Application.isPlaying || inEditor)
        {

            if (material != null && mesh != null)
            {
                renderer.EnqueuePass(_FogPass);
                _FogPass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
            }
        }
    }

    public override void Create()
    {
        _FogPass = new FogPass(material,mesh);
    }

    // Start is called before the first frame update
  
}
