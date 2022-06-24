using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public class PlayingState : State
    {
        public PlayingState(IPlayerAPI playerApi) : base(playerApi)
        { }

        public override void Update(bool isTapped, float deltaTime)
        {
            base.Update(isTapped, deltaTime);

            if (playerApi.isCollidedWithObstacle || playerApi.isCollidedWithGround)
            {
                playerApi.SwitchToState(new LoosedState(playerApi));
                return;
            }

            if (playerApi.isFinishReached)
            {
                playerApi.SwitchToState(new WinnedState(playerApi));
                return;
            }

            if (isTapped)
            {
                playerApi.rigidbody2d.AddForce(new Vector2(0f, playerApi.upForce * deltaTime));
                playerApi.animController.SetIsFlying(true);
            }
            else
            {
                playerApi.rigidbody2d.AddForce(new Vector2(0f, -playerApi.downForce * deltaTime));
                playerApi.animController.SetIsFlying(false);
            }
            playerApi.rigidbody2d.velocity = new Vector2(playerApi.xVelocity, playerApi.rigidbody2d.velocity.y);

            playerApi.SetRotationByVelocity();
        }

        public override void OnExit(State newState)
        {
            playerApi.animController.SetIsFlying(false);

            base.OnExit(newState);
        }
    }
}
