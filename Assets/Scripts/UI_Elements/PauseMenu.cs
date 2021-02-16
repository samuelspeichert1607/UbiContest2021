using UnityEngine;

namespace UI_Elements
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenu;
        
        [SerializeField]
        private GameObject validationMenu;
    
        private bool _isPaused = false;

        public void Update()
        {
            //TODO there is a better way to map this probably
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPaused = !_isPaused;
                if (validationMenu.activeSelf)
                {
                    validationMenu.SetActive(false);
                }
            }

            if (_isPaused)
            {
                TriggerPauseOn();
            }
            else
            {
                TriggerPauseOff();
            }
        }

        public void TriggerPauseOn()
        {
            pauseMenu.SetActive(true);
            _isPaused = true;
        }
    
        public void TriggerPauseOff()
        {
            pauseMenu.SetActive(false);
            _isPaused = false;
        }

        public void LogOut()
        {
            Debug.Log("Logging out");
            // SceneManager.LoadScene(0); //Laoding back the menu scene
        }
    
    }
}
