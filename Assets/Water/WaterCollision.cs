using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    public float causticOffsetSpeed = 0.5f; // Speed of caustic texture offset
    private Material waterMaterial;
    private Vector2 currentOffset = Vector2.zero;

    void Start()
    {
        waterMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        currentOffset += new Vector2(causticOffsetSpeed, causticOffsetSpeed) * Time.deltaTime;
        waterMaterial.SetTextureOffset("_CausticTex", currentOffset);
    }
}
