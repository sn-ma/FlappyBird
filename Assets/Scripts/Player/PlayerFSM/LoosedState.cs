using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public class LoosedState : State
    {
        private bool wasPlayerStoppedOnThePrevFrame = false;

        public LoosedState(IPlayerApi playerApi) : base(playerApi)
        { }

        public override void OnEnter(State oldState)
        {
            base.OnEnter(oldState);

            playerApi.animController.SetDead();
        }

        public override void Update(bool isTapped, float deltaTime)
        {
            base.Update(isTapped, deltaTime);

            PlayerApiUtils.ApplyGravity(playerApi, deltaTime);

            bool isPlayerStopped = PlayerApiUtils.IsPlayerStopped(playerApi);

            if (isPlayerStopped && (wasPlayerStoppedOnThePrevFrame || playerApi.isCollidedWithGround))
            {
                playerApi.SwitchToState(new DelayedQuitState(playerApi, playerApi.delayBeforeExitOnLoose, null));
            }
            wasPlayerStoppedOnThePrevFrame = isPlayerStopped;
        }
    }
}
