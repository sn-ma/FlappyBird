using System;
using UnityEngine;

namespace FlappyBirdClone
{
    public class SoundSwitcher : MonoBehaviour
    {
        public event Action<bool> onSoundSwithced;

        public bool IsSoundSwitchedOn
        {
            get => isSoundSwitchedOn;
            set
            {
                isSoundSwitchedOn = value;
                AudioListener.pause = !isSoundSwitchedOn;
                PlayerPrefs.SetInt(prefName, isSoundSwitchedOn ? 1 : 0);
                onSoundSwithced?.Invoke(isSoundSwitchedOn);
            }
        }

        private bool isSoundSwitchedOn;

        private const string prefName = "soundOn";

        void Start()
        {
            bool soundShouldBeOn = PlayerPrefs.GetInt(prefName, 1) != 0;
            IsSoundSwitchedOn = soundShouldBeOn;
        }
    }
}
