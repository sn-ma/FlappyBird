using FlappyBirdClone.Player.FSM;
using UnityEngine;

namespace FlappyBirdClone.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float upForce;
        [SerializeField]
        private float downForce;
        [SerializeField]
        public float xGoalVelocity;
        [SerializeField]
        public float xMaxForce;
        [SerializeField]
        private float delayBeforeExitOnWin;
        [SerializeField]
        private float delayBeforeExitOnLoose;

        [SerializeField]
        private string stateReadOnly = "none";


        private Rigidbody2D rigidbody2d;
        private PlayerAnimController animController;
        private PlayerApiImpl playerApi;

        private State state = null;

        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            animController = GetComponent<PlayerAnimController>();
            playerApi = new PlayerApiImpl(this);
            playerApi.SwitchToState(new PlayingState(playerApi));
        }


        void Update()
        {
            state.Update(Input.touchCount > 0, Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Finish":
                    playerApi.isFinishReached = true;
                    break;
                default:
                    Debug.LogError($"Triggered by object with unexpected type: {collision.tag}");
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Obstacle":
                    playerApi.isCollidedWithObstacle = true;
                    break;
                case "Ground":
                    playerApi.isCollidedWithGround = true;
                    break;
                default:
                    Debug.LogError($"Triggered by object with unexpected type: {collision.gameObject.tag}");
                    break;
            }
        }

        private class PlayerApiImpl : IPlayerApi
        {
            private readonly PlayerController playerController;

            public PlayerApiImpl(PlayerController playerController)
            {
                this.playerController = playerController;
            }

            // Fields stored in this class
            public bool isCollidedWithObstacle { get; set; }
            public bool isCollidedWithGround { get; set; }
            public bool isFinishReached { get; set; }

            // Stored in the PlayerController:
            public float upForce { get => playerController.upForce; }
            public float downForce { get => playerController.downForce; }
            public float xGoalVelocity { get => playerController.xGoalVelocity; }
            public float xAcceleration { get => playerController.xMaxForce; }
            public float delayBeforeExitOnWin { get => playerController.delayBeforeExitOnWin; }
            public float delayBeforeExitOnLoose { get => playerController.delayBeforeExitOnLoose; }
            public Rigidbody2D rigidbody2d { get => playerController.rigidbody2d; }
            public PlayerAnimController animController { get => playerController.animController; }

            public void SwitchToState(State newState)
            {
                playerController.stateReadOnly = newState.ToString();

                State oldState = playerController.state;
                oldState?.OnExit(newState);
                playerController.state = newState;
                newState.OnEnter(oldState);
            }
        }
    }
}