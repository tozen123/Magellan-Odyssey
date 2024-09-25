using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffectManager : MonoBehaviour
{
    public static PlayerSoundEffectManager instance;

    public AudioSource audioSource;
    public AudioSource audioSourceTheme;

    public AudioClip ConvoPop_Clip;
    public AudioClip NextDialogue_Clip;

    public AudioClip CorrectQuiz_Clip;
    public AudioClip WrongQuiz_Clip;


    public AudioClip CrossBowShot_Clip;

    public AudioClip PlayerWalking_Clip;


    public AudioClip PlayerQuizTheme_Clip;
    public AudioClip PickUp_Clip;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }
    public static void PlayPickUp()
    {
        if (instance != null && instance.PickUp_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.PickUp_Clip);
        }

    }
    public static void PlayQuizTheme()
    {
        if (instance != null && instance.PlayerQuizTheme_Clip != null)
        {
            instance.audioSourceTheme.PlayOneShot(instance.PlayerQuizTheme_Clip);
        }

    }
    public static void StopQuizTheme()
    {
        if (instance != null && instance.PlayerQuizTheme_Clip != null)
        {
            instance.audioSourceTheme.Stop();
        }

    }

    public static void PlayConvoPopUp()
    {
        if (instance != null && instance.ConvoPop_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.ConvoPop_Clip);
        }

    }

    public static void PlayNextDialogue()
    {
        if (instance != null && instance.NextDialogue_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.NextDialogue_Clip);
        }

    }
    public static void PlayWrongQuiz()
    {
        if (instance != null && instance.WrongQuiz_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.WrongQuiz_Clip);
        }

    }
    public static void PlayCorrectQuiz()
    {
        if (instance != null && instance.CorrectQuiz_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.CorrectQuiz_Clip);
        }

    }

    public static void PlayCrossBowShot()
    {
        if (instance != null && instance.CrossBowShot_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.CrossBowShot_Clip);
        }

    }

    public static void PlayPlayerWalking()
    {
        if (instance != null && instance.PlayerWalking_Clip != null)
        {
            instance.audioSource.volume = 0.3f;
            instance.audioSource.PlayOneShot(instance.PlayerWalking_Clip);
        }
    }
    public static void PlayPlayerWalkingStop()
    {
        if (instance != null && instance.PlayerWalking_Clip != null)
        {
            instance.audioSource.Stop();
            instance.audioSource.volume = 0.9f; // Reset volume to 0.9
        }
    }
}
