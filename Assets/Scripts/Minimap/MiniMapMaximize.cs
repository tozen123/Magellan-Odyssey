using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapMaximize : MonoBehaviour
{
    public Button panelTap;
    private bool isToggled = false;

    public RectTransform panel;
    [SerializeField] private Vector2 originalSize;
    [SerializeField] private Vector2 resizeSize = new Vector2(1280, 780);
    [SerializeField] private Vector2 originalAnchorMin;
    [SerializeField] private Vector2 originalAnchorMax;
    [SerializeField] private Vector2 maximizedAnchorMin = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 maximizedAnchorMax = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 minimizedAnchorMin = new Vector2(1f, 1f);
    [SerializeField] private Vector2 minimizedAnchorMax = new Vector2(1f, 1f);
    [SerializeField] private Vector2 minimizedPositionOffset = new Vector2(-300, -300);

    public RenderTexture minimap;
    public RenderTexture minimapfull;

    public RawImage image;


    private GameObject cameraMiniMapFull;

    void Start()
    {
        originalSize = panel.sizeDelta;
        originalAnchorMin = panel.anchorMin;
        originalAnchorMax = panel.anchorMax;
        panelTap.onClick.AddListener(TogglePanel);

        cameraMiniMapFull = GameObject.FindGameObjectWithTag("MiniMapCameraFullMapView").gameObject;
        cameraMiniMapFull.SetActive(false);

    }

    private void TogglePanel()
    {
        isToggled = !isToggled;
        SoundEffectManager.PlayButtonClick2();

        if (isToggled)
        {
            image.texture = minimapfull;
            cameraMiniMapFull.SetActive(true);

            panel.sizeDelta = resizeSize;
            panel.anchorMin = maximizedAnchorMin;
            panel.anchorMax = maximizedAnchorMax;
            panel.anchoredPosition = Vector2.zero;
        }
        else
        {
            image.texture = minimap;

            cameraMiniMapFull.SetActive(false);

            panel.sizeDelta = originalSize;
            panel.anchorMin = minimizedAnchorMin;
            panel.anchorMax = minimizedAnchorMax;
            panel.anchoredPosition = minimizedPositionOffset;
        }
    }

    private void OnDestroy()
    {
        panelTap.onClick.RemoveListener(TogglePanel);
    }
}
