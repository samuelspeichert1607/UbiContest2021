using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void ClickStartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }
}
