namespace FlappyBirdClone.Player.FSM
{
    public class State
    {
        protected IPlayerApi playerApi;

        public State(IPlayerApi playerApi)
        {
            this.playerApi = playerApi;
        }

        public virtual void Update(bool isTapped, float deltaTime) { }

        public virtual void OnEnter(State oldState) { }

        public virtual void OnExit(State newState) { }
    }
}