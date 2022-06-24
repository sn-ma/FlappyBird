using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public interface IPlayerAPI
    {
        public bool isCollidedWithObstacle { get; }
        public bool isCollidedWithGround { get; }
        public bool isFinishReached { get; }
        public float upForce { get; }
        public float downForce { get; }
        public float xVelocity { get; }
        public float delayBeforeExitOnWin { get; }
        public float delayBeforeExitOnLoose { get; }
        public Rigidbody2D rigidbody2d { get; }
        public PlayerAnimController animController { get; }

        public void SetRotationByVelocity()
        {
            Vector2 velocity = rigidbody2d.velocity;
            if (Mathf.Abs(velocity.x) > 0.001f && velocity.sqrMagnitude > 0.001f)
            {
                rigidbody2d.rotation = Mathf.Rad2Deg * Mathf.Atan(velocity.y / velocity.x);
            }
        }

        public void SwitchToState(State newState);
    }
}