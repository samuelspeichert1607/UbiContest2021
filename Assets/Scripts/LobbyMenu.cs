using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyMenu : MonoBehaviour
{
    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void ClickReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
