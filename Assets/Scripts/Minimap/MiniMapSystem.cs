using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMapSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject MiniMap;

    public DialogueManager dialogue;

    private void Update()
    {
        if (dialogue.isDialogueActive)
        {
            MiniMap.SetActive(false);
        } else
        {
            MiniMap.SetActive(true);    
        }
    }




    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 40, player.transform.position.z);
    }
}