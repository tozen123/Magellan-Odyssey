using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelTwoHandler : MonoBehaviour
{
    void Start()
    {
        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("You accompanied Ferdinand Magellan to the Royal Palace")
               .Show();
    }

    void Update()
    {
        
    }
}
