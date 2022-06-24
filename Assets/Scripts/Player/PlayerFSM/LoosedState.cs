using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public class LoosedState : State
    {
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

            if (playerApi.isCollidedWithGround && PlayerApiUtils.IsPlayerStopped(playerApi))
            {
                playerApi.SwitchToState(new DelayedQuitState(playerApi, playerApi.delayBeforeExitOnLoose, null));
            }
        }
    }
}
