using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    private AudioSource audioSource;

    public AudioClip ButtonClick2_Clip;
    public AudioClip CardPopUp_Clip;
    public AudioClip Reward_Clip;
    public AudioClip Notice_Clip;
    public AudioClip QuestCompleteReward_Clip;

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
    
    public static void PlayButtonClick2()
    {
        if (instance != null && instance.ButtonClick2_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.ButtonClick2_Clip);
        }
        
    }
    public static void PlayButtonCardPopup()
    {
        if (instance != null && instance.CardPopUp_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.CardPopUp_Clip);
        }

    }

    public static void PlayReward()
    {
        if (instance != null && instance.Reward_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.Reward_Clip);
        }

    }

    public static void PlayNotice()
    {
        if (instance != null && instance.Notice_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.Notice_Clip);
        }

    }


    public static void PlayQuestCompleteReward()
    {
        if (instance != null && instance.QuestCompleteReward_Clip != null)
        {
            instance.audioSource.PlayOneShot(instance.QuestCompleteReward_Clip);
        }

    }

    public static void SetButtonClickClip(AudioClip newClip)
    {
        if (instance != null)
        {
            instance.ButtonClick2_Clip = newClip;
        }
    }
}
