using FlappyBirdClone.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdClone
{
    public class FollowXAxis : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private float shift = 0f;

        [SerializeField]
        private bool useAcceleration = true;

        [SerializeField]
        private float maxAcceleration = 0.01f;

        [SerializeField]
        private PlayerController playerController;

        private float currentVelocity = 0f;

        private void Start()
        {
            Vector3 position = transform.position;
            position.x = target.position.x + shift;
            transform.position = position;
            currentVelocity = playerController.xGoalVelocity;
        }

        private void Update()
        {
            Vector3 position = transform.position;
            float targetPositionX = target.position.x + shift;
            if (!useAcceleration)
            {
                position.x = targetPositionX;
            } else
            {
                float availableAcceleration = maxAcceleration * Time.deltaTime;
                if (targetPositionX > position.x) // Speed up
                {
                    if (position.x + currentVelocity + availableAcceleration < targetPositionX)
                    {
                        currentVelocity += availableAcceleration;
                    } else
                    {
                        currentVelocity += targetPositionX - currentVelocity - position.x;
                    }
                } else // Slow down
                {
                    if (position.x + currentVelocity - availableAcceleration > targetPositionX)
                    {
                        currentVelocity -= availableAcceleration;
                    }
                    else
                    {
                        currentVelocity += targetPositionX - currentVelocity - position.x;
                    }
                }
                position.x += currentVelocity * Time.deltaTime;
            }
            transform.position = position;
        }
    }
}