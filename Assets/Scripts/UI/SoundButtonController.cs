using UnityEngine;
using UnityEngine.UI;

namespace FlappyBirdClone.UI
{
    [RequireComponent(typeof(Image))]
    public class SoundButtonController : MonoBehaviour
    {
        [SerializeField]
        private SoundSwitcher soundSwitcher;

        [SerializeField]
        private Sprite onIcon;
        
        [SerializeField]
        private Sprite offIcon;

        private Image image;

        void Start()
        {
            image = GetComponent<Image>();

            soundSwitcher.onSoundSwithced += ToggleIcon;
            ToggleIcon(soundSwitcher.IsSoundSwitchedOn);
        }

        public void SwitchSound()
        {
            soundSwitcher.IsSoundSwitchedOn = !soundSwitcher.IsSoundSwitchedOn;
        }

        private void ToggleIcon(bool isSoundSwitchedOn)
        {
            image.sprite = isSoundSwitchedOn ? onIcon : offIcon;
        }
    }
}