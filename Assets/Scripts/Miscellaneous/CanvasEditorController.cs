using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEditorController : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject TARGET_CANVAS;
    private void Awake()
    {
        TARGET_CANVAS.gameObject.SetActive(true);
    }
}
