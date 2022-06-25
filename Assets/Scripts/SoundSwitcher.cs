using UnityEngine;
using UnityEngine.Events;

namespace FlappyBirdClone
{
    public class SoundSwitcher : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onSoundSwitchedOn;

        [SerializeField]
        private UnityEvent onSoundSwitchedOff;

        private const string prefName = "soundOn";

        void Start()
        {
            bool soundShouldBeOn = PlayerPrefs.GetInt(prefName, 1) != 0;
            if (soundShouldBeOn)
            {
                SwitchOn();
            }
            else
            {
                SwitchOff();
            }
        }

        public void SwitchOn()
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt(prefName, 1);
            onSoundSwitchedOn?.Invoke();
        }

        public void SwitchOff()
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt(prefName, 0);
            onSoundSwitchedOff?.Invoke();
        }

        public void Toggle()
        {
            if (AudioListener.pause)
            {
                SwitchOn();
            } else
            {
                SwitchOff();
            }
        }
    }
}
