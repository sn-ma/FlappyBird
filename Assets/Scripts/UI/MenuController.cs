using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonClickSound;

        [SerializeField]
        private List<MenuPageDescr> pages;

        private static bool wasLoaded = false;

        private void Start()
        {
            if (!wasLoaded)
            {
                Pause();
                ShowMenuPage("BeforeGame");
                wasLoaded = true;
            }
        }

        [Serializable]
        public struct MenuPageDescr
        {
            public string name;
            public GameObject pageObject;
        }

        public void ShowMenuPage(string name)
        {
            foreach (MenuPageDescr page in pages)
            {
                page.pageObject.SetActive(page.name == name);
            }
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            Debug.Log("Pausing");
        }

        public void Unpause()
        {
            Time.timeScale = 1f;
            Debug.Log("Unpausing");
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void PlayButtonClickSound()
        {
            if (buttonClickSound != null)
            {
                Instantiate(buttonClickSound, transform.position, Quaternion.identity, null);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}