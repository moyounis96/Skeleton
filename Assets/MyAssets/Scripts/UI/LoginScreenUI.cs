using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScreenUI : MonoBehaviour
{
    public static LoginScreenUI Instance;

    public TMP_InputField email;
    public TMP_InputField password;
    public UITransition container;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        TryAutoLogin();
    }

    bool isExiting = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isExiting)
            {
                Debug.Log("Quit");
                Application.Quit();
            }
            isExiting = true;
            ILogger.Instance.ShowMessage("Press back again to leave!", LoggerType.warning);
            Invoke("SetIsExiting", 1f);
        }
    }
    void SetIsExiting()
    {
        isExiting = false;
    }
    public void TryAutoLogin()
    {
        if(AuthManager.Instance.User == null || string.IsNullOrEmpty(AuthManager.Instance.User.Email))
        {
            email.text = PlayerPrefs.GetString(Constants.EMAIL_PREFS, "");
            password.text = PlayerPrefs.GetString(Constants.PASSWORD_PREFS, "");
            if (!string.IsNullOrEmpty(email.text))
            {
                Login();
            }
            else
            {
                LoadingScreen.Instance.Hide();
            }
        }
        else
        {
            AfterLogin();
        }
        
    }
    public void Login()
    {
        if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(password.text))
        {
            ILogger.Instance.ShowMessage("Please fill in your <B>email or phone number</B> and <B>password</B>");
            return;
        }
        StartCoroutine(AuthManager.Instance.Login(email.text, password.text));
    }
    public void AfterLogin()
    {
        LoadingScreen.Instance.Show("Loading Game...",SceneManager.LoadSceneAsync(1),true);
    }
}
