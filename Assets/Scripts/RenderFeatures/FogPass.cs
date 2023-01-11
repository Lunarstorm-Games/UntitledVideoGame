using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FogPass : ScriptableRenderPass
{
    private Material _material;
    private Mesh _mesh;
    
    public FogPass(Material material, Mesh mesh)
    {
        _material = material;
        _mesh = mesh;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get(name: "FogPass");

        Camera camera = renderingData.cameraData.camera;

        var currentRt = BuiltinRenderTextureType.CameraTarget;
        // Set the projection matrix so that Unity draws the quad in screen space
        cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
        // Add the scale variable, use the Camera aspect ration for the y coordinate
        Vector3 scale = new Vector3(camera.pixelWidth, camera.pixelHeight, 1);

        cmd.DrawMesh(_mesh, Matrix4x4.TRS(new Vector3(0,0,0), Quaternion.identity,scale),
               _material, 0, 0);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd); 
    }

    // Start is called before the first frame update
   
}
