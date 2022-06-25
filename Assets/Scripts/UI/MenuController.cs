using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone
{
    public class MenuController : MonoBehaviour
    {
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
        }

        public void Unpause()
        {
            Time.timeScale = 1f;
        }

        public void SwitchSound()
        {
            AudioListener.pause = !AudioListener.pause;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}