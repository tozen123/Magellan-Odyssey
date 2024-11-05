using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLineRenderer : MonoBehaviour
{
    public Transform player;
    private LineRenderer lineRenderer;

    private int currentPointIndex = 0; 
    public float thresholdDistance = 1.0f;  

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer.positionCount > 1)
        {
            lineRenderer.SetPosition(0, player.position);
        }
    }

    void Update()
    {

        Vector3 nextPoint = lineRenderer.GetPosition(1); 

        if (Vector3.Distance(player.position, nextPoint) <= thresholdDistance)
        {
            RemoveReachedPoint();
        }

        lineRenderer.SetPosition(0, player.position);
    }

    private void RemoveReachedPoint()
    {
        int pointCount = lineRenderer.positionCount;

        for (int i = 1; i < pointCount; i++)
        {
            lineRenderer.SetPosition(i - 1, lineRenderer.GetPosition(i));
        }

        lineRenderer.positionCount--;
    }
}
