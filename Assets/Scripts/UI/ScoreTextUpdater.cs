using TMPro;
using UnityEngine;

namespace FlappyBirdClone.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreTextUpdater : MonoBehaviour
    {
        private TextMeshProUGUI text;

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void SetScore(int score)
        {
            text.text = score.ToString();
        }
    }
}