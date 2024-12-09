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
       .SetMessage("Bago tayo magsimula sa ating paglalakbay, ipakikilala muna namin ang mga controls at gameplay.")
       .Show();

        DialogMessagePrompt.Instance
              .SetTitle("How to Play: Controls")
              .SetMessage("Ito ang Movement Joystick, magagamit mo ito upang tumingin at gumalaw sa isang partikular na direksyon.")
              .SetImage(movementjoystick)
              .Show();

        DialogMessagePrompt.Instance
              .SetTitle("How to Play: Controls")
              .SetMessage("Ito ang Attack Joystick, magagamit mo ito upang tumutok at magpaputok ng mga projectiles sa mga kalaban o mga bagay na kailangang basagin." +
              "Tandaan na ang avatar ay gumagamit ng crossbow.")
              .SetImage(attackjoystick)
              .Show();

        DialogMessagePrompt.Instance
              .SetTitle("How to Play: Controls")
              .SetMessage("Ito ang Interact Button, magagamit mo ito upang makipag-ugnayan sa mga karakter sa laro. Tandaan na hindi lahat ng karakter ay maaaring makausap. " +
              "Ang button ay lalaking at lilitaw upang ipakita na ang isang partikular na karakter ay maaaring makipag-ugnayan.")
              .SetImage(interact)
              .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Controls")
             .SetMessage("Gamitin din ang Interact Button upang matapos ang iyong mga misyon sa laro.")
             .SetImage(interact)
             .Show();

        DialogMessagePrompt.Instance
            .SetTitle("How to Play: Controls")
            .SetMessage("Ito ang Pick Up Button, magagamit mo ito upang kunin ang mga bagay na maaaring ilagay sa iyong imbentaryo. " +
            "Ang button na ito ay lalaking at lilitaw upang ipakita na ang bagay na iyong nilapitan ay maaaring kunin.")
            .SetImage(pickup)
            .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Interfaces")
             .SetMessage("Ito ang iyong MiniMap. Gamitin ito upang matukoy ang iyong kasalukuyang lokasyon sa mapa at upang mahanap ang direksyon patungo sa mga partikular na karakter o lugar.")
             .SetImage(map)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Interfaces")
             .SetMessage("Maari ring i-click ang MiniMap upang palakihin ito para sa mas maayos na pagtingin.")
             .SetImage(map)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Interfaces")
             .SetMessage("Ito ang iyong Healthbar. Nagsisilbi itong batayan ng kalusugan ng iyong avatar. Panatilihing hindi ito bumaba sa zero upang hindi ma-reset ang iyong gameplay sa isang partikular na kabanata.")
             .SetImage(health)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Interfaces")
             .SetMessage("Ito ang Book Of Trivia Button. Magagamit mo ito upang balikan ang mahahalagang puntos ng Kabanata. Maari mo itong i-unlock sa pamamagitan ng pagtatapos ng lahat ng misyon maliban sa Quiz Masters.")
             .SetImage(bot)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Interfaces")
             .SetMessage("Ito ang Inventory Button. Magagamit mo ito upang makita ang iyong imbentaryo.")
             .SetImage(inv)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Interfaces")
             .SetMessage("Ito ang Quest List Button. Gamitin ito upang makita ang iyong mga misyon.")
             .SetImage(questlist)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Interfaces")
             .SetMessage("Ito ang Settings, gamitin ito kung nais mong lumabas sa laro o baguhin ang tunog.")
             .SetImage(settings)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Gameplay")
             .SetMessage("Ngayon, magpatuloy tayo sa gameplay.")
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Gameplay")
             .SetMessage("Maari kang lumapit o makipag-ugnayan sa mga karakter sa laro upang tapusin ang partikular na mga misyon na magbibigay sa iyo ng mga puntos!")
             .SetImage(tut_in)
             .Show();

        DialogMessagePrompt.Instance
             .SetTitle("How to Play: Gameplay")
             .SetMessage("Iyon na ang lahat para sa ngayon. Ikaw na ang bahala! Goodluck!")
             .Show();


        //DialogMessagePrompt.Instance
        //       .SetTitle("System Message")
        //       .SetMessage("Before we start our adventure, lets introduce you to the controls and gameplay.")
        //       .Show();

        //DialogMessagePrompt.Instance
        //      .SetTitle("How to Play: Controls")
        //      .SetMessage("This is Movement Joystick, you can use this to look and move to an specific direction.")
        //      .SetImage(movementjoystick)
        //      .Show();

        //DialogMessagePrompt.Instance
        //      .SetTitle("How to Play: Controls")
        //      .SetMessage("This is Attack Joystick, you can use this to aim and shoot projectiles to enemy or objects that needed to be break." +
        //      "take note that the avatar uses crossbow.")
        //      .SetImage(movementjoystick)
        //      .Show();

        //DialogMessagePrompt.Instance
        //      .SetTitle("How to Play: Controls")
        //      .SetMessage("This is Interact Button, you can use this to interact to in game characters. But keep in mind that some characters in the game " +
        //      "are not interatable. The button will be scaling up and down visualizing that an specific character can be interact. ")
        //      .SetImage(interact)
        //      .Show();

        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Controls")
        //     .SetMessage("Also, use the interact button to finish your quests in the game.")
        //     .SetImage(interact)
        //     .Show();

        //DialogMessagePrompt.Instance
        //    .SetTitle("How to Play: Controls")
        //    .SetMessage("This is the Pick Up Button, you can use this to pick up objects that can be pick up and be put into your inventory. " +
        //    "this button will scaling up and down visualizing that the object you approached is pickable.")
        //    .SetImage(pickup)
        //    .Show();


        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Interfaces")
        //     .SetMessage("This is your MiniMap. utilize this to determine your current location in the map and to find direction towards specific character or points.")
        //     .SetImage(map)
        //     .Show();

        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Interfaces")
        //     .SetMessage("The MiniMap can be also be click to maximize for better viewing.")
        //     .SetImage(map)
        //     .Show();


        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Interfaces")
        //     .SetMessage("This is your Healthbar. it serves as your basis for checking your avatar health. keep this below zero to not restart your gameplay on specific kabanata.")
        //     .SetImage(health)
        //     .Show();

        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Interfaces")
        //     .SetMessage("This is Book Of Trivia Button. You can use this to review important points of the Kabanata. You can unlock them by finishing all the quest apart from the Quiz Masters")
        //     .SetImage(bot)
        //     .Show();

        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Interfaces")
        //     .SetMessage("This is inventory button, you can use this to view your inventory.")
        //     .SetImage(inv)
        //     .Show();

        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Interfaces")
        //     .SetMessage("This is Quest List Button. Use this to view your quest")
        //     .SetImage(questlist)
        //     .Show();

        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Interfaces")
        //     .SetMessage("This is settings, use this whenever you want to exit the game or adjust the sounds")
        //     .SetImage(settings)
        //     .Show();



        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Gameplay")
        //     .SetMessage("Now, lets move on the gameplay. ")
        //     .Show();


        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Gameplay")
        //     .SetMessage("You can go towards or approach characters in the game to interact, by doing that you can complete specific quests in the game granting you points!")
        //     .SetImage(tut_in)
        //     .Show();

        //DialogMessagePrompt.Instance
        //     .SetTitle("How to Play: Gameplay")
        //     .SetMessage("That's all for now, you're on your own now! Goodluck")
        //     .Show();
    }

    public void Pointer1()
    {

    }
}
