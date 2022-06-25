namespace FlappyBirdClone.Player.FSM
{
    public class GoToEogMenuState : DelayedTransitionState
    {
        public GoToEogMenuState(IPlayerApi playerApi, float delay) : base(playerApi, delay)
        {}

        protected override void MakeTransition()
        {
            playerApi.GoToEogMenu();
            playerApi.SwitchToState(new IdleState(playerApi));
        }
    }
}
