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

        public void Start()
        {
            // photonView = GetComponent<PhotonView>();
            pauseMenu.SetActive(false);
            playerController = GetComponentInParent<CustomController>();
        }

        public void Update()
        {
            // if (photonView.IsMine)
            // {
                //TODO there is a better way to map this probably
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
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
            pauseMenu.SetActive(true);
            playerController.disableMovement();
            SelectObject(onPauseFirstSelected);
        }

        private void UnPause()
        {
            playerController.allowMovement();
            pauseMenu.SetActive(false);
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
