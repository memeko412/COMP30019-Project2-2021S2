using UnityEngine;
using System.Collections;


[ExecuteInEditMode]

[RequireComponent(typeof(Camera))]

public class PostManager : MonoBehaviour
{


    public Shader shader = null;
    private Material _material = null;


    [Range(0.0f, 3.0f)]
    public float brightness = 1.0f;
    [Range(0.0f, 3.0f)]
    public float contrast = 1.0f;  
    [Range(0.0f, 3.0f)]
    public float saturation = 1.0f;

    public Material _Material
    {
        get
        {
            if (_material == null)
                _material = GenerateMaterial(shader);
            return _material;
        }
    }


    protected Material GenerateMaterial(Shader shader)
    {
        if (shader == null)
            return null;
        if (shader.isSupported == false)
            return null;
        Material material = new Material(shader);
        material.hideFlags = HideFlags.DontSave;
        if (material)
            return material;
        return null;
    }



    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {

        if (_Material)
        {
            _Material.SetFloat("_Brightness", brightness);
            _Material.SetFloat("_Saturation", saturation);
            _Material.SetFloat("_Contrast", contrast);
            Graphics.Blit(src, dest, _Material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }


}