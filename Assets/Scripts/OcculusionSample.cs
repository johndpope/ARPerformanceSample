﻿using UnityEngine;
using UnityEngine.XR.ARFoundation;

/// <summary>
/// stencilTextureをshaderにセットし、shader処理結果を画面に出力する
/// OnRenderImageを使用するため、Cameraコンポーネントが必要(AR Camera)にアタッチされる想定
/// </summary>
[RequireComponent(typeof(Camera))]
public class OcculusionSample : MonoBehaviour
{
    [SerializeField]
    AROcclusionManager occlusionManager;

    [SerializeField]
    Shader shader;

    // debug用のstencilTexture
    [SerializeField]
    Texture2D debugTex;
    Material material;

    void Awake()
    {
        material = new Material(shader);
    }
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
#if UNITY_EDITOR
        // UnityEditor上でのdebug用動作。debugTexをstencilTexとして使う
        material.SetTexture("_StencilTex", debugTex);
#else 
        // 端末上での動作
        if (occlusionManager != null)
        {
            material.SetTexture("_StencilTex", occlusionManager.humanStencilTexture);
        }
#endif
        Graphics.Blit(src, dest, material);
    }
}