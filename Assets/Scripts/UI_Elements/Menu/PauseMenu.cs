using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Photon.Pun;

namespace UI_Elements
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenu;
        // private PhotonView photonView;
        private CustomController playerController;
        
        [SerializeField]
        private GameObject validationMenu;

        [SerializeField] 
        private GameObject onPauseFirstSelected, onValidationFirstSelected, onReturnFromValidationFirstSelected;
        
        private AudioPlayerMenu _audioPlayer;
        private ControllerManager _controllerManager;
        private GameObject _currentlySelected;
        private bool isAtfirstOpeningFrame;

        public void Start()
        {
            _controllerManager = GetComponent<ControllerManager>();
            _audioPlayer = GetComponent<AudioPlayerMenu>();
            // photonView = GetComponent<PhotonView>();
            pauseMenu.SetActive(false);
            playerController = GetComponentInParent<CustomController>();
        }

        public void Update()
        {
            //TODO there is a better way to map this probably
            //// if (photonView.IsMine)
            // {
            if (Input.GetKeyDown(KeyCode.Escape) || _controllerManager.GetButtonDown("Start")
                && !playerController.IsInCriticalMotion())
            {
                pauseUnPause();
                if (validationMenu.activeSelf)
                {
                    validationMenu.SetActive(false);
                }
            }
            if (HasNavigatedInMenu())
            {
                _audioPlayer.PlayButtonNavigationSound();
            }
            _currentlySelected = EventSystem.current.currentSelectedGameObject;
            if (isAtfirstOpeningFrame) isAtfirstOpeningFrame = false;
            
            // }
        }

        private bool HasNavigatedInMenu()
        {
            return _currentlySelected != EventSystem.current.currentSelectedGameObject && !isAtfirstOpeningFrame;
        }
        
        

        public void pauseUnPause()
        {
            if (!pauseMenu.activeInHierarchy)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }

        private void Pause()
        {
            playerController.disableMovement();
            pauseMenu.SetActive(true);
            SelectObject(onPauseFirstSelected);
            isAtfirstOpeningFrame = true;
        }

        private void UnPause()
        {
            pauseMenu.SetActive(false);
            Invoke(nameof(AllowPlayerMovement), 0.05f);
        }

        private void AllowPlayerMovement()
        {
            playerController.allowMovement();
        }

        public void LogOut()
        {
            Debug.Log("Logging out");
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel(1);
        }

        public void OpenValidationMenu()
        {
            validationMenu.SetActive(true);
            SelectObject(onValidationFirstSelected);
        }

        public void CloseValidationMenu()
        {
            validationMenu.SetActive(false);
            SelectObject(onReturnFromValidationFirstSelected);
        }
        

        private void SelectObject(GameObject gameObjectToSelect)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObjectToSelect);
        }
        
    }
}
