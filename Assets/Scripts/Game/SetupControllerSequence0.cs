using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class SetupControllerSequence0 : MonoBehaviour
{
    [Header("Creation Inputs")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI email;
    public TextMeshProUGUI password;
    public TextMeshProUGUI confirm_password;

    [Header("Login Inputs")]
    public TextMeshProUGUI loginEmail;
    public TextMeshProUGUI loginPassword;

    [Header("Avatar Canvas")]
    public GameObject creationCanvas;
    public GameObject loginCanvas;

    private CanvasGroup creationCanvasGroup;
    private CanvasGroup loginCanvasGroup;

    void Start()
    {
        Application.targetFrameRate = 60;

        creationCanvasGroup = creationCanvas.GetComponent<CanvasGroup>();
        loginCanvasGroup = loginCanvas.GetComponent<CanvasGroup>();

        creationCanvas.SetActive(false);
        loginCanvas.SetActive(false);

        DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Welcome to Magellan Odyssey")
                .Show();

        if (PlayerPrefs.HasKey("userEmail") && PlayerPrefs.HasKey("userPassword"))
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Welcome back, Player. " + PlayerPrefs.GetString("userName"))
                .OnClose(AutomaticLogin)
                .Show();
        }
        else
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Hello, Player. Before you start the game, you need to have an avatar.")
                .OnClose(ShowCreationAvatarCanvas)
                .Show();
        }
    }

    void Update() { }

    void ShowCreationAvatarCanvas()
    {
        creationCanvas.SetActive(true);
        StartCoroutine(FadeCanvasGroup(creationCanvasGroup, 0, 1, 1f));
    }

    void ShowLoginCanvas()
    {
        loginCanvas.SetActive(true);
        StartCoroutine(FadeCanvasGroup(loginCanvasGroup, 0, 1, 1f));
    }

    public void CreateAvatar()
    {
        string avatar_name = name.text.Trim();
        string avatar_email = email.text.Trim();
        string avatar_password = password.text.Trim();
        string avatar_confirm_password = confirm_password.text.Trim();

        if (string.IsNullOrEmpty(avatar_name))
        {
            DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Name cannot be left empty.")
               .Show();
            return;
        }

        if (!IsValidEmail(avatar_email))
        {
            DialogMessagePrompt.Instance
               .SetTitle("System Message")
               .SetMessage("Your email does not seem to be valid.")
               .Show();
            return;
        }

        if (avatar_password != avatar_confirm_password)
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Passwords do not match!")
                .Show();
            return;
        }

        Debug.Log(avatar_name);
        Debug.Log(avatar_email);
        Debug.Log(avatar_password);
        Debug.Log(avatar_confirm_password);

        string userID = System.Guid.NewGuid().ToString();

        //User user = new User(avatar_name, avatar_email, userID, avatar_password);
        //user.WriteUserToDatabase();

        PlayerPrefs.SetString("userName", avatar_name);
        PlayerPrefs.SetString("userEmail", avatar_email);
        PlayerPrefs.SetString("userPassword", avatar_password);
        PlayerPrefs.SetString("userID", userID);
        PlayerPrefs.Save();

        DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("Account created successfully!")
            .OnClose(Finish)
            .Show();
    }

    public void AutomaticLogin()
    {
        if (PlayerPrefs.HasKey("userEmail") && PlayerPrefs.HasKey("userPassword"))
        {
            string email = PlayerPrefs.GetString("userEmail").Trim();
            string password = PlayerPrefs.GetString("userPassword").Trim();

            LoginAvatar(email, password);
        }
        else
        {
            DialogMessagePrompt.Instance
            .SetTitle("System Message")
            .SetMessage("Saved Login not found")
            
            .Show();

            creationCanvas.SetActive(true);
        }
            

    }
    public void ManualLogin()
    {
        LoginAvatar(loginEmail.text, loginPassword.text);


    }
    public async void LoginAvatar(string login_email, string login_password)
    {
       

        //User user = await User.CheckUser(loginEmail.text.Trim().ToString(), loginPassword.text.Trim().ToString());
        //OnLoginResult(user);
    }

    private void OnLoginResult(User user)
    {
        if (user != null)
        {
            //PlayerPrefs.SetString("userName", user.name);
            //PlayerPrefs.SetString("userEmail", user.email);
            //PlayerPrefs.SetString("userPassword", user.password);
            //PlayerPrefs.SetString("userID", user.userID);
            //PlayerPrefs.Save();

            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Login successful!")
                .OnClose(Finish)
                .Show();
        }
        else
        {
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("Invalid email or password!")
                .Show();

            PlayerPrefs.DeleteAll();
            DialogMessagePrompt.Instance
                .SetTitle("System Message")
                .SetMessage("An error occured in connecting to database. Closing the game")
                .Show();
            Application.Quit();
        }
    }

    public void OpenLoginCanvas()
    {
        StartCoroutine(SwitchCanvas(creationCanvasGroup, loginCanvasGroup));
    }

    public void OpenCreationCanvas()
    {
        StartCoroutine(SwitchCanvas(loginCanvasGroup, creationCanvasGroup));
    }

    public void Finish()
    {
        LoadingScreenManager.Instance.LoadScene("MainMenu-Sequence1");
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float elapsed = Time.time - startTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        canvasGroup.interactable = (endAlpha == 1);
        canvasGroup.blocksRaycasts = (endAlpha == 1);
    }

    private IEnumerator SwitchCanvas(CanvasGroup fromCanvas, CanvasGroup toCanvas)
    {
        yield return StartCoroutine(FadeCanvasGroup(fromCanvas, 1, 0, 0.3f));
        fromCanvas.gameObject.SetActive(false);
        toCanvas.gameObject.SetActive(true);
        yield return StartCoroutine(FadeCanvasGroup(toCanvas, 0, 1, 0.3f));
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }
}
