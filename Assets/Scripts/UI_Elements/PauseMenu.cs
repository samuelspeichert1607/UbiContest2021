using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

        private ControllerPicker controllerPicker;
        private IController userController;

        public void Start()
        {
            controllerPicker = new ControllerPicker();
            pauseMenu.SetActive(false);
        }

        public void Update()
        {
            PickController();
            //TODO there is a better way to map this probably
            if (Input.GetKeyDown(KeyCode.Escape) || userController.GetButtonDown("Button7"))
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
            // SceneManager.LoadScene(0); //Laoding back the menu scene
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
