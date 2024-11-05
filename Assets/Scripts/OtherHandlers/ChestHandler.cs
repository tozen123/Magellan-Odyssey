using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : MonoBehaviour
{
    private ChapterOneLevelFiveHandler handler;
    private void Start()
    {
        handler = GameObject.FindObjectOfType<ChapterOneLevelFiveHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            handler.OnChestFind(this.gameObject);


            

            Destroy(this.gameObject);
        }
    }
}
