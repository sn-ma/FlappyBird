using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonClickSound;

        [SerializeField]
        private GameObject[] pages;

        public void ShowMenuPage(int selectIndex)
        {
            for (int i = 0; i < pages.Length; ++i)
            {
                pages[i].SetActive(i == selectIndex);
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