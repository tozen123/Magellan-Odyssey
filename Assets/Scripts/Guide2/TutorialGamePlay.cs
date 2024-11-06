using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGamePlay : MonoBehaviour
{

    public Sprite movementjoystick;
    public Sprite attackjoystick;

    public Sprite interact;
    public Sprite pickup;


    public Sprite map;

    public Sprite health;
    public Sprite adp;
    public Sprite acp;


    public Sprite bot;
    public Sprite inv;
    public Sprite questlist;
    public Sprite settings;



    public Sprite tut_in;



    void Start()
    {

        if (PlayerPrefs.HasKey("HasSeenTutorial2"))
        {
            if (PlayerPrefs.GetString("HasSeenTutorial2", "No") == "No")
            {
                Debug.Log("YEAH: 1");

       
                PlayerPrefs.SetString("HasSeenTutorial2", "Yes");
                PlayerPrefs.Save();


            }
            else
            {
                Debug.Log("YEAH: 2");

           

                return;

            }

        }
        else
        {
            Debug.Log("YEAH: 3");

            PlayerPrefs.SetString("HasSeenTutorial2", "Yes");
            PlayerPrefs.Save();

        }




        DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Before we start our adventure, lets introduce you to the controls and gameplay.")
               .Show();

        DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("This is Movement Joystick, you can use this to look and move to an specific direction.")
              .SetImage(movementjoystick)
              .Show();

        DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("This is Attack Joystick, you can use this to aim and shoot projectiles to enemy or objects that needed to be break." +
              "take note that the avatar uses crossbow.")
              .SetImage(movementjoystick)
              .Show();

        DialogMessagePrompt.Instance
              .SetTitle("System Message")
              .SetMessage("This is Interact Button, you can use this to interact to in game characters. But keep in mind that some characters in the game " +
              "are not interatable. The button will be scaling up and down visualizing that an specific character can be interact. ")
              .SetImage(interact)
              .Show();

        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("Also, use the interact button to finish your quests in the game.")
             .SetImage(interact)
             .Show();

        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("This is the Pick Up Button, you can use this to pick up objects that can be pick up and be put into your inventory. " +
            "this button will scaling up and down visualizing that the object you approached is pickable.")
            .SetImage(pickup)
            .Show();


        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("This is your MiniMap. utilize this to determine your current location in the map and to find direction towards specific character or points.")
             .SetImage(map)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("The MiniMap can be also be click to maximize for better viewing.")
             .SetImage(map)
             .Show();


        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("This is your Healthbar. it serves as your basis for checking your avatar health. keep this below zero to not restart your gameplay on specific kabanata.")
             .SetImage(health)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("This is Book Of Trivia Button. You can use this to review important points of the Kabanata. You can unlock them by finishing all the quest apart from the Quiz Masters")
             .SetImage(bot)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("This is inventory button, you can use this to view your inventory.")
             .SetImage(inv)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("This is Quest List Button. Use this to view your quest")
             .SetImage(questlist)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("This is settings, use this whenever you want to exit the game or adjust the sounds")
             .SetImage(settings)
             .Show();



        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("Now, lets move on the gameplay. ")
             .Show();


        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("You can go towards or approach characters in the game to interact, by doing that you can complete specific quests in the game granting you points!")
             .SetImage(tut_in)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("System Message")
             .SetMessage("That's all for now, you're on your own now! Goodluck")
             .Show();
    }

    public void Pointer1()
    {

    }
}
