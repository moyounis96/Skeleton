using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOutBtn : MonoBehaviour
{
    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        LoadingScreen.Instance.Show("Logging Out", SceneManager.LoadSceneAsync(0), true);
    }
    bool isExiting = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isExiting)
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
}
