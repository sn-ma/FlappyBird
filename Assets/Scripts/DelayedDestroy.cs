using UnityEngine;

namespace FlappyBirdClone
{
    public class DelayedDestroy : MonoBehaviour
    {
        [SerializeField]
        private float timeLeft = 1;

        void Update()
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}