using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public interface IPlayerApi
    {
        public bool isCollidedWithObstacle { get; }
        public bool isCollidedWithGround { get; }
        public bool isFinishReached { get; }
        public float upForce { get; }
        public float downForce { get; }
        public float xGoalVelocity { get; }
        public float xAcceleration { get; }
        public float delayBeforeExitOnWin { get; }
        public float delayBeforeExitOnLoose { get; }
        public Rigidbody2D rigidbody2d { get; }
        public PlayerAnimController animController { get; }
        public GameEffects gameEffects { get; }

        public void SwitchToState(State newState);

        public void GoToEogMenu();
    }
}