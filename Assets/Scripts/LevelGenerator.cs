using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FlappyBirdClone
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject obstaclePrefab;

        [SerializeField]
        private GameObject finishPrefab;

        [SerializeField]
        private float startPositionX;

        [SerializeField]
        private List<GenerationSetting> settings;

        [SerializeField]
        private UnityEvent onGenerationFinished;

        void Start()
        {
            float positionX = startPositionX;

            foreach(GenerationSetting setting in settings)
            {
                for (int i = 0; i < setting.count; ++i)
                {
                    GameObject obstacle = Instantiate(obstaclePrefab, transform);
                    obstacle.transform.localPosition = new Vector3(positionX, UnityEngine.Random.Range(setting.verticalShiftLimits.x, setting.verticalShiftLimits.y), 0f);

                    float verticalDistance = UnityEngine.Random.Range(setting.verticalDistanceLimits.x, setting.verticalDistanceLimits.y);
                    List <SpriteRenderer> children = obstacle.GetComponentsInChildren<SpriteRenderer>().ToList();
                    children.Sort((first, second) => first.transform.position.y.CompareTo(second.transform.position.y));
                    children[0].transform.localPosition = new Vector3(0f, verticalDistance / -2f, 0f);
                    children[1].transform.localPosition = new Vector3(0f, verticalDistance / 2f, 0f);


                    positionX += UnityEngine.Random.Range(setting.horizontalDistanceLimits.x, setting.horizontalDistanceLimits.y);
                }
            }

            Instantiate(finishPrefab, new Vector3(positionX, 0f, 0f), Quaternion.identity, null);

            onGenerationFinished?.Invoke();
        }

        [Serializable]
        public class GenerationSetting
        {
            [SerializeField]
            public int count;

            [SerializeField]
            public Vector2 verticalDistanceLimits;

            [SerializeField]
            public Vector2 verticalShiftLimits;

            [SerializeField]
            public Vector2 horizontalDistanceLimits;
        }
    }
}