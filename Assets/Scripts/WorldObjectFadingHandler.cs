using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectFadingHandler : MonoBehaviour
{
    [SerializeField] private float fadingSpeed = 2.0f;
    [SerializeField] private float fadingAmount = 0.3f;

    private Dictionary<Renderer, float> _originalOpacities = new Dictionary<Renderer, float>();
    private Renderer[] _renderers;

    public bool activeFade;
    private float _fadeTimer = 2f;
    private bool _isFading = false;

    void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in _renderers)
        {
            // Store original opacity
            _originalOpacities[renderer] = renderer.material.color.a;

            // Set material to transparent mode
            SetMaterialTransparent(renderer.material);
        }
    }

    void Update()
    {
        if (activeFade)
        {
            if (!_isFading)
            {
                StartFade();
            }

            _fadeTimer -= Time.deltaTime;

            if (_fadeTimer <= 0f)
            {
                activeFade = false;
                _fadeTimer = 2f;
            }
        }
        else
        {
            if (_isFading)
            {
                ResetFade();
            }
        }
    }

    private void StartFade()
    {
        _isFading = true;
        StopAllCoroutines(); // Stop any existing reset coroutine
        StartCoroutine(FadeTo(fadingAmount));
    }

    private void ResetFade()
    {
        _isFading = false;
        StopAllCoroutines(); // Stop any existing fade coroutine
        StartCoroutine(FadeToOriginal());
    }

    private IEnumerator FadeTo(float targetOpacity)
    {
        float elapsed = 0f;
        while (elapsed < fadingSpeed)
        {
            foreach (var renderer in _renderers)
            {
                Color currentColor = renderer.material.color;
                float newAlpha = Mathf.Lerp(currentColor.a, targetOpacity, elapsed / fadingSpeed);
                renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha is set exactly to target opacity
        foreach (var renderer in _renderers)
        {
            Color currentColor = renderer.material.color;
            renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, targetOpacity);
        }
    }

    private IEnumerator FadeToOriginal()
    {
        float elapsed = 0f;
        while (elapsed < fadingSpeed)
        {
            foreach (var renderer in _renderers)
            {
                Color currentColor = renderer.material.color;
                float originalOpacity = _originalOpacities[renderer];
                float newAlpha = Mathf.Lerp(currentColor.a, originalOpacity, elapsed / fadingSpeed);
                renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha is set exactly to the original opacity
        foreach (var renderer in _renderers)
        {
            Color currentColor = renderer.material.color;
            renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, _originalOpacities[renderer]);
        }
    }

    private void SetMaterialTransparent(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}
