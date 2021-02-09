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

        [SerializeField] private GameObject onPauseFirstSelected, onValidationFirstSelection;

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
                
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(onPauseFirstSelected);
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
    
    }
}
