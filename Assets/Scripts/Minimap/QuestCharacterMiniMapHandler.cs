using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCharacterMiniMapHandler : MonoBehaviour
{

    public Canvas myCanvas;
    void Start()
    {

        Camera camera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
        myCanvas.worldCamera = camera;
    }

   
}
