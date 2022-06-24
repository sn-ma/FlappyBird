namespace FlappyBirdClone.Player.FSM
{
    public abstract class DelayedTransitionState : State
    {
        private float timeLeft;

        public DelayedTransitionState(IPlayerAPI playerApi, float delay) : base(playerApi)
        {
            timeLeft = delay;
        }

        public override void Update(bool isTapped, float deltaTime)
        {
            base.Update(isTapped, deltaTime);

            timeLeft -= deltaTime;
            if (timeLeft <= 0f)
            {
                MakeTransition();
            }
        }

        protected abstract void MakeTransition();
    }
}
