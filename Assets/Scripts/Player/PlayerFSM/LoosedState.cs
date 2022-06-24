using UnityEngine;

namespace FlappyBirdClone.Player.FSM
{
    public class LoosedState : State
    {
        public LoosedState(IPlayerAPI playerApi) : base(playerApi)
        { }

        public override void OnEnter(State oldState)
        {
            base.OnEnter(oldState);

            playerApi.animController.SetDead();
        }

        public override void Update(bool isTapped, float deltaTime)
        {
            base.Update(isTapped, deltaTime);

            playerApi.rigidbody2d.AddForce(new Vector2(0f, -playerApi.downForce * deltaTime));

            if (playerApi.rigidbody2d.velocity.sqrMagnitude < 0.001f)
            {
                playerApi.SwitchToState(new DelayedQuitState(playerApi, playerApi.delayBeforeExitOnLoose, null));
            }
        }
    }
}
