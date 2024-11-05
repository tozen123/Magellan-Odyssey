using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrompterWall : MonoBehaviour
{
    [SerializeField] private string title;
    [SerializeField] [TextArea] private string message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DialogMessagePrompt.Instance
               .SetTitle(title)
               .SetMessage(message)
               .Show();
        }
    }
    
}
