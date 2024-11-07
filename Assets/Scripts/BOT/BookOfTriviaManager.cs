using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookOfTriviaManager : MonoBehaviour
{
    public Button buttonBookOfTrivia;
    private bool isToggled = false;

    public GameObject panel;

    public Button kabanata1;
    public Button kabanata2;
    public Button kabanata3;
    public Button kabanata4;
    public Button kabanata5;
    public Button kabanata6;

    private Color activeColor = new Color(0.74f, 0.49f, 0.23f);
    private Color defaultColor;

    public GameObject kabanataPanelView1;
    public GameObject kabanataPanelView2;
    public GameObject kabanataPanelView3;
    public GameObject kabanataPanelView4;
    public GameObject kabanataPanelView5;
    public GameObject kabanataPanelView6;

    public GameObject kabanataPanelViewLock;

    public TextMeshProUGUI lockText;

    void Start()
    {
        defaultColor = kabanata1.GetComponent<Image>().color;


        kabanata1.onClick.AddListener(() => Kabanata(kabanata1, "Kabanata1BookOfTrivia_IsLock", 1));
        kabanata2.onClick.AddListener(() => Kabanata(kabanata2, "Kabanata2BookOfTrivia_IsLock", 2));
        kabanata3.onClick.AddListener(() => Kabanata(kabanata3, "Kabanata3BookOfTrivia_IsLock", 3));
        kabanata4.onClick.AddListener(() => Kabanata(kabanata4, "Kabanata4BookOfTrivia_IsLock", 4));
        kabanata5.onClick.AddListener(() => Kabanata(kabanata5, "Kabanata5BookOfTrivia_IsLock", 5));
        kabanata6.onClick.AddListener(() => Kabanata(kabanata6, "Kabanata6BookOfTrivia_IsLock", 6));

        panel.SetActive(false);
    }

    public void Kabanata(Button selectedButton, string lockKey, int kabanataNumber)
    {
        SoundEffectManager.PlayButtonClick2();

        ResetButtonColors();
        selectedButton.GetComponent<Image>().color = activeColor;

        bool isLocked = PlayerPrefs.GetInt(lockKey, 1) == 1;

        if (isLocked)
        {
            lockText.text = $"Tapusin ang mga quest ng Kabanata {kabanataNumber} para ma-unlock";
            ShowPanel(kabanataPanelViewLock);
        }
        else
        {
            if (selectedButton == kabanata1)
            {
                ShowPanel(kabanataPanelView1);
            }
            else if (selectedButton == kabanata2)
            {
                ShowPanel(kabanataPanelView2);
            }
            else if (selectedButton == kabanata3)
            {
                ShowPanel(kabanataPanelView3);
            }
            else if (selectedButton == kabanata4)
            {
                ShowPanel(kabanataPanelView4);
            }
            else if (selectedButton == kabanata5)
            {
                ShowPanel(kabanataPanelView5);
            }
            else if (selectedButton == kabanata6)
            {
                ShowPanel(kabanataPanelView6);
            }
        }
    }

    public void TogglePanel()
    {
        isToggled = !isToggled;
        SoundEffectManager.PlayButtonClick2();

        panel.SetActive(isToggled);

        if (isToggled)
        {
            bool isKabanata1Locked = PlayerPrefs.GetInt("Kabanata1BookOfTrivia_IsLock", 1) == 1;
            if (!isKabanata1Locked)
            {
                ResetButtonColors();

                kabanata1.GetComponent<Image>().color = activeColor;

                ShowPanel(kabanataPanelView1);
            }
            else
            {
                kabanata1.GetComponent<Image>().color = activeColor;

                lockText.text = "Tapusin ang mga quest ng Kabanata 1 para ma-unlock";
                ShowPanel(kabanataPanelViewLock);
            }
        }
    }

    private void ResetButtonColors()
    {
        kabanata1.GetComponent<Image>().color = defaultColor;
        kabanata2.GetComponent<Image>().color = defaultColor;
        kabanata3.GetComponent<Image>().color = defaultColor;
        kabanata4.GetComponent<Image>().color = defaultColor;
        kabanata5.GetComponent<Image>().color = defaultColor;
        kabanata6.GetComponent<Image>().color = defaultColor;
    }

    private void ShowPanel(GameObject panelToShow)
    {
        kabanataPanelView1.SetActive(false);
        kabanataPanelView2.SetActive(false);
        kabanataPanelView3.SetActive(false);
        kabanataPanelView4.SetActive(false);
        kabanataPanelView5.SetActive(false);
        kabanataPanelView6.SetActive(false);
        kabanataPanelViewLock.SetActive(false);

        panelToShow.SetActive(true);
    }

    public void UnlockKabanata(string lockKey)
    {
        PlayerPrefs.SetInt(lockKey, 0);
        PlayerPrefs.Save();
    }
}
