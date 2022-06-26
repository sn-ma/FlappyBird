using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdClone
{
    public class DisableOnPause : MonoBehaviour
    {
        private void Update()
        {
            if (Time.timeScale == 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
