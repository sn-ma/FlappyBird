using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public class WinnedState : DelayedTransitionState
    {
        public WinnedState(IPlayerApi playerApi) : base(playerApi, playerApi.delayBeforeExitOnWin)
        { }

        public override void OnEnter(State oldState)
        {
            base.OnEnter(oldState);

            PlayerApiUtils.CleanRotationAndVelocityOnFinish(playerApi);

            Camera.main.GetComponent<FollowXAxis>().enabled = false;

            playerApi.animController.SetIsFlying(true);
            playerApi.gameEffects.Win();
        }

        protected override void MakeTransition()
        {
            playerApi.SwitchToState(new DelayedQuitState(playerApi, 0f, null));
        }
    }
}
