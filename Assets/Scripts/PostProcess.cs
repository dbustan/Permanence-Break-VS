using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    public Shader shader;
    private Material material;

    void Start() {
        material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, material);
    }
}
