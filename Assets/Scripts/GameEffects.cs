using UnityEngine;

namespace FlappyBirdClone
{
    public class GameEffects : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameOverEffect;

        [SerializeField]
        private GameObject winEffect;

        public void GameOver()
        {
            if (gameOverEffect != null)
            {
                Instantiate(gameOverEffect, transform.position, Quaternion.identity, null);
            }
        }
        public void Win()
        {
            if (winEffect != null)
            {
                Instantiate(winEffect, transform.position, Quaternion.identity, null);
            }
        }
    }
}