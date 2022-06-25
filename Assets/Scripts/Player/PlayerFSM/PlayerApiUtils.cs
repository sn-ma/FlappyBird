using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public static class PlayerApiUtils
    {
        public static void SetRotationByVelocity(IPlayerApi playerAPI)
        {
            Vector2 velocity = playerAPI.rigidbody2d.velocity;
            if (Mathf.Abs(velocity.x) > 0.001f && velocity.sqrMagnitude > 0.001f)
            {
                playerAPI.rigidbody2d.rotation = Mathf.Rad2Deg * Mathf.Atan(velocity.y / velocity.x);
            }
        }

        public static void CleanRotationAndVelocityOnFinish(IPlayerApi playerApi)
        {
            playerApi.rigidbody2d.rotation = 0f;
            playerApi.rigidbody2d.velocity = new Vector2(playerApi.xGoalVelocity, 0f);
        }

        public static void ApplyGravity(IPlayerApi playerApi, float deltaTime)
        {
            playerApi.rigidbody2d.AddForce(new Vector2(0f, -playerApi.downForce * deltaTime));
        }

        public static void ApplyUpAcceleration(IPlayerApi playerApi, float deltaTime)
        {
            playerApi.rigidbody2d.AddForce(new Vector2(0f, playerApi.upForce * deltaTime));
        }

        public static void AccelerateToGoalXVelocity(IPlayerApi playerApi, float deltaTime)
        {
            if (playerApi.rigidbody2d.velocity.x < playerApi.xGoalVelocity)
            {
                Vector2 velocity = playerApi.rigidbody2d.velocity;
                velocity.x = Mathf.Min(velocity.x + playerApi.xAcceleration * deltaTime / playerApi.rigidbody2d.mass, playerApi.xGoalVelocity);
                playerApi.rigidbody2d.velocity = velocity;
            }
        }

        public static bool IsPlayerStopped(IPlayerApi playerApi)
        {
            return playerApi.rigidbody2d.velocity.sqrMagnitude < 0.001f;
        }
    }
}