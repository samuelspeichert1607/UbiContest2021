using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private GameObject optionMenu, creditMenu;
    
    [SerializeField]
    private GameObject onMenuOpenFirstSelected, onOptionsFirstSelected, onCreditsFirstSelected;
    
    public void ClickStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void ClickOptionsBtn()
    {
        optionMenu.SetActive(true);
        SelectObject(onOptionsFirstSelected);
    }

    public void openMenu()
    {
        SelectObject(onMenuOpenFirstSelected);
    }
    
    public void ClickCreditsBtn()
    {
        creditMenu.SetActive(true);
        SelectObject(onCreditsFirstSelected);
    }
    
    private void SelectObject(GameObject gameObjectToSelect)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
    }
}
