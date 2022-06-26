namespace FlappyBirdClone.Player.FSM
{
    public class PlayingState : State
    {
        public PlayingState(IPlayerApi playerApi) : base(playerApi)
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
                PlayerApiUtils.ApplyUpAcceleration(playerApi, deltaTime);
                playerApi.animController.SetIsFlying(true);
                playerApi.gameEffects.WingsEffectOn();
            }
            else
            {
                PlayerApiUtils.ApplyGravity(playerApi, deltaTime);
                playerApi.animController.SetIsFlying(false);
                playerApi.gameEffects.WingsEffectOff();
            }
            PlayerApiUtils.AccelerateToGoalXVelocity(playerApi, deltaTime);

            PlayerApiUtils.SetRotationByVelocity(playerApi);
        }

        public override void OnExit(State newState)
        {
            playerApi.animController.SetIsFlying(false);

            base.OnExit(newState);
        }
    }
}
