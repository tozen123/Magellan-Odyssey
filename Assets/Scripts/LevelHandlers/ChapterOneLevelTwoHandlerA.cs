using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterOneLevelTwoHandlerA : MonoBehaviour
{

    public Animator cutSceneAnimation;
    public Animator cutSceneAnimationMag;
    public Animator cutSceneAnimationPlayer;

    public GameObject introAnimItems;
    public GameObject mag;
    public GameObject player;



    void Start()
    {


       

   
    }

    void Update()
    {
        
    }
    public void Game()
    {
        cutSceneAnimation.SetTrigger("start");
    }
    public void EndCutscene()
    {
        cutSceneAnimationMag.SetTrigger("bow");
        cutSceneAnimationPlayer.SetTrigger("bow");
    }

    public void AllDone()
    {
        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Ikaw at si Ferdinand Magellan ay nag luhod sa harap ni King Manoel I")
               .OnClose(BackToGame)
               .Show();
    }

    void BackToGame()
    {


    
        mag.SetActive(true);
        player.SetActive(true);

        introAnimItems.SetActive(false);
        

    }
}
