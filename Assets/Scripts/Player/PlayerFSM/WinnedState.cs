using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public class WinnedState : DelayedTransitionState
    {
        public WinnedState(IPlayerAPI playerApi) : base(playerApi, playerApi.delayBeforeExitOnWin)
        { }

        public override void OnEnter(State oldState)
        {
            base.OnEnter(oldState);

            playerApi.rigidbody2d.rotation = 0f;
            playerApi.rigidbody2d.velocity = new Vector2(playerApi.xVelocity, 0f);

            Camera.main.GetComponent<FollowXAxis>().enabled = false;

            playerApi.animController.SetIsFlying(true);
        }

        protected override void MakeTransition()
        {
            playerApi.SwitchToState(new DelayedQuitState(playerApi, 0f, null));
        }
    }
}
