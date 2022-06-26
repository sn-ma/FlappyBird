using UnityEngine;

namespace FlappyBirdClone
{
    public class GameEffects : MonoBehaviour
    {
        [SerializeField]
        private GameObject gameOverEffect;

        [SerializeField]
        private GameObject winEffect;

        [SerializeField]
        private GameObject scoreIncEffect;

        [SerializeField]
        private GameObject wingsEffectInstance;

        public void GameOver() => PlayEffectIfAny(gameOverEffect);
        public void Win() => PlayEffectIfAny(winEffect);
        public void ScoreInc() => PlayEffectIfAny(scoreIncEffect);
        public void WingsEffectOn()
        {
            wingsEffectInstance.SetActive(true);
        }
        public void WingsEffectOff()
        {
            wingsEffectInstance.SetActive(false);
        }

        private void PlayEffectIfAny(GameObject effect)
        {
            if (effect != null)
            {
                Instantiate(effect, transform.position, Quaternion.identity, null);
            }
        }
    }
}