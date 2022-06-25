using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyBirdClone
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField]
        private bool calculateOnStartup = false;

        [SerializeField]
        private UnityEvent<int> onScoreChanged;

        [SerializeField]
        private Transform player;

        public int score { get; private set; }

        private LinkedList<float> scorePositions = new LinkedList<float>();

        private void Start()
        {
            if (calculateOnStartup)
            {
                RecalculateScorePositions();
            }
        }

        public void RecalculateScorePositions()
        {
            ScoreMarker[] markers = GetComponentsInChildren<ScoreMarker>();
            List<float> temp = new List<float>(markers.Length);
            foreach (ScoreMarker marker in markers)
            {
                temp.Add(marker.transform.position.x);
            }
            temp.Sort();
            foreach (float pos in temp)
            {
                scorePositions.AddLast(pos);
            }
        }

        void Update()
        {
            while (scorePositions.Count > 0 && player.position.x > scorePositions.First.Value)
            {
                IncScore();
                scorePositions.RemoveFirst();
            }
        }

        private void IncScore()
        {
            ++score;
            onScoreChanged?.Invoke(score);
        }

        public void ResetScore()
        {
            score = 0;
            onScoreChanged?.Invoke(score);
        }
    }
}