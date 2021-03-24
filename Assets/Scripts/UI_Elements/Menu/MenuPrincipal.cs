using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private GameObject controlsMenu, creditMenu;
    
    [SerializeField]
    private GameObject onMenuOpenFirstSelected, onControlsFirstSelected, onCreditsFirstSelected;

    [SerializeField] private AudioManagerMenu _audioManager;
    
    private GameObject _currentlySelected;
    private bool isAtfirstOpeningFrame = true;

    public void Start()
    {
        OpenMenu();
    }
    public void OpenMenu()
    {
        _currentlySelected = onControlsFirstSelected;
        SelectObject(onMenuOpenFirstSelected);
        controlsMenu.SetActive(false);
        isAtfirstOpeningFrame = true;
    }
    
    public void Update()
    {
        if (HasNavigatedInMenu())
        {
            _audioManager.PlayButtonNavigationSound();
        }
        _currentlySelected = EventSystem.current.currentSelectedGameObject;
        if (isAtfirstOpeningFrame) isAtfirstOpeningFrame = false;
    }

    private bool HasNavigatedInMenu()
    {
        return _currentlySelected != EventSystem.current.currentSelectedGameObject && !isAtfirstOpeningFrame;
    }

    public void ClickPlayButton()
    {
        _audioManager.PlayClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickQuitButton()
    {
        _audioManager.PlayClickSound();
        Application.Quit();
    }

    public void ClickControlsBtn()
    {
        _audioManager.PlayClickSound();
        controlsMenu.SetActive(true);
        SelectObject(onControlsFirstSelected);
    }

    
    public void ClickCreditsBtn()
    {
        _audioManager.PlayClickSound();
        creditMenu.SetActive(true);
        SelectObject(onCreditsFirstSelected);
    }
    
    private void SelectObject(GameObject gameObjectToSelect)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
    }
    
}
