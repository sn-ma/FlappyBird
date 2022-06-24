using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone.Player.FSM
{
    public class DelayedQuitState : DelayedTransitionState
    {
        private string sceneToLoad;

        public DelayedQuitState(IPlayerAPI playerApi, float delay, string sceneToLoad) : base(playerApi, delay)
        {
            this.sceneToLoad = sceneToLoad;
        }

        protected override void MakeTransition()
        {
            Debug.Log("Quiting");
            if (sceneToLoad != null)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
