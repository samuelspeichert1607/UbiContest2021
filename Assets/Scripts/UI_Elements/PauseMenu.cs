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
        
        [SerializeField]
        private GameObject validationMenu;

        [SerializeField] 
        private GameObject onPauseFirstSelected, onValidationFirstSelected, onReturnFromValidationFirstSelected;

        public void Start()
        {
            pauseMenu.SetActive(false);
        }

        public void Update()
        {
            //TODO there is a better way to map this probably
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                pauseUnPause();
                if (validationMenu.activeSelf)
                {
                    validationMenu.SetActive(false);
                }
            }
        }

        public void pauseUnPause()
        {
            if (!pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(true);
                SelectObject(onPauseFirstSelected);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
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
