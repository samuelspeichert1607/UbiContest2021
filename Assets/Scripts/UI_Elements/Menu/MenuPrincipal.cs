using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField]
    private GameObject controlsMenu, creditMenu;

    [SerializeField]
    private GameObject onMenuOpenFirstSelected, onControlsFirstSelected, onCreditsFirstSelected, onReturnFromControlsFirstSelected, onReturnFromCreditsFirstSelected;

    [SerializeField] private AudioPlayerMenu audioPlayer;
    
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
        creditMenu.SetActive(false);
        isAtfirstOpeningFrame = true;
    }
    
    public void Update()
    {
        if (HasNavigatedInMenu())
        {
            audioPlayer.PlayButtonNavigationSound();
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
        audioPlayer.PlayClickSound();
        Invoke(nameof(PlayButtonPressed), 0.02f);
    }

    private void PlayButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickQuitButton()
    {
        audioPlayer.PlayClickSound();
        Application.Quit();
    }

    public void ClickControlsBtn()
    {
        audioPlayer.PlayClickSound();
        controlsMenu.SetActive(true);
        SelectObject(onControlsFirstSelected);
    }
    
    public void ClickCreditsBtn()
    {
        audioPlayer.PlayClickSound();
        creditMenu.SetActive(true);
        SelectObject(onCreditsFirstSelected);
    }
    
    private void SelectObject(GameObject gameObjectToSelect)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
    }
    
    public void CloseCreditsMenu()
    {
        _currentlySelected = onReturnFromCreditsFirstSelected;
        SelectObject(onReturnFromCreditsFirstSelected);
        creditMenu.SetActive(false);
        isAtfirstOpeningFrame = true;
    }

    public void CloseControlsMenu()
    {
        _currentlySelected = onReturnFromControlsFirstSelected;
        SelectObject(onReturnFromControlsFirstSelected);
        controlsMenu.SetActive(false);
        isAtfirstOpeningFrame = true;
    }
}
