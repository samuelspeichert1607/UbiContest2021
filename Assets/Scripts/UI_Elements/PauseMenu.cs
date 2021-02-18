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

        private ControllerPicker controllerPicker;
        private IController userController;

        public void Start()
        {
            controllerPicker = new ControllerPicker();
            // photonView = GetComponent<PhotonView>();
            pauseMenu.SetActive(false);
            playerController = GetComponentInParent<CustomController>();
        }

        public void Update()
        {
            PickController();
            //TODO there is a better way to map this probably
            //// if (photonView.IsMine)
            // {
            if (Input.GetKeyDown(KeyCode.Escape) || userController.GetButtonDown("Button7"))
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
    
        private void PickController()
        {
            string currentController = Input.GetJoystickNames()[0];
            if (controllerPicker.IsDifferentController(currentController))
            {
                userController = controllerPicker.PickController(currentController);
            }
        }
    }
}
