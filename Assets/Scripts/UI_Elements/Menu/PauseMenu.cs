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

        private ControllerManager controllerManager;

        public void Start()
        {
            controllerManager = GetComponent<ControllerManager>();
            // photonView = GetComponent<PhotonView>();
            pauseMenu.SetActive(false);
            playerController = GetComponentInParent<CustomController>();
        }

        public void Update()
        {
            //TODO there is a better way to map this probably
            //// if (photonView.IsMine)
            // {
            if (Input.GetKeyDown(KeyCode.Escape) || controllerManager.GetButtonDown("Start"))
            {
                pauseUnPause();
                if (validationMenu.activeSelf)
                {
                    validationMenu.SetActive(false);
                }
            } 
            // }
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
            PhotonNetwork.LoadLevel(0);
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
