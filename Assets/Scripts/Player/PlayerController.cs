using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float upForce;
    [SerializeField]
    private float downForce;
    [SerializeField]
    private float xVelocity;
    [SerializeField]
    private float delayBeforeExitOnWin;
    [SerializeField]
    private float delayBeforeExitOnLoose;

    [SerializeField]
    private string stateReadOnly = "none";


    private Rigidbody2D rigidbody2d;
    private PlayerAnimController animController;

    private State state = null;

    private bool isCollidedWithObstacle = false;
    private bool isCollidedWithGround = false;
    private bool isFinishReached = false;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animController = GetComponent<PlayerAnimController>();
        SwitchToState(new PlayingState(this));
    }


    void Update()
    {
        state.Update(Input.touchCount > 0, Time.deltaTime);
    }

    private void SetRotationByVelocity()
    {
        Vector2 velocity = rigidbody2d.velocity;
        if (Mathf.Abs(velocity.x) > 0.001f && velocity.sqrMagnitude > 0.001f)
        {
            rigidbody2d.rotation = Mathf.Rad2Deg * Mathf.Atan(velocity.y / velocity.x);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Finish":
                isFinishReached = true;
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
                isCollidedWithObstacle = true;
                break;
            case "Ground":
                isCollidedWithGround = true;
                break;
            default:
                Debug.LogError($"Triggered by object with unexpected type: {collision.gameObject.tag}");
                break;
        }
    }

    private void SwitchToState(State newState)
    {
        stateReadOnly = newState.ToString();
        State oldState = state;
        oldState?.OnExit(newState);
        state = newState;
        newState.OnEnter(oldState);
    }

    private abstract class State
    {
        protected PlayerController controller;

        public State(PlayerController controller)
        {
            this.controller = controller;
        }

        public virtual void Update(bool isTapped, float deltaTime) { }

        public virtual void OnEnter(State oldState) { }

        public virtual void OnExit(State newState) { }
    }

    private class PlayingState : State
    {
        public PlayingState(PlayerController controller) : base(controller)
        { }

        public override void Update(bool isTapped, float deltaTime)
        {
            base.Update(isTapped, deltaTime);

            if (controller.isCollidedWithObstacle || controller.isCollidedWithGround)
            {
                controller.SwitchToState(new LoosedState(controller));
                return;
            }

            if (controller.isFinishReached)
            {
                controller.SwitchToState(new WinnedState(controller));
                return;
            }

            if (isTapped)
            {
                controller.rigidbody2d.AddForce(new Vector2(0f, controller.upForce * deltaTime));
                controller.animController.SetIsFlying(true);
            }
            else
            {
                controller.rigidbody2d.AddForce(new Vector2(0f, -controller.downForce * deltaTime));
                controller.animController.SetIsFlying(false);
            }
            controller.rigidbody2d.velocity = new Vector2(controller.xVelocity, controller.rigidbody2d.velocity.y);

            controller.SetRotationByVelocity();
        }

        public override void OnExit(State newState)
        {
            controller.animController.SetIsFlying(false);

            base.OnExit(newState);
        }
    }

    private abstract class DelayedTransitionState : State
    {
        private float timeLeft;

        public DelayedTransitionState(PlayerController controller, float delay) : base(controller)
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

    private class WinnedState : DelayedTransitionState
    {
        public WinnedState(PlayerController controller) : base(controller, controller.delayBeforeExitOnWin)
        { }

        public override void OnEnter(State oldState)
        {
            base.OnEnter(oldState);

            controller.rigidbody2d.rotation = 0f;
            controller.rigidbody2d.velocity = new Vector2(controller.xVelocity, 0f);

            Camera.main.GetComponent<FollowXAxis>().enabled = false;

            controller.animController.SetIsFlying(true);
        }

        protected override void MakeTransition()
        {
            controller.SwitchToState(new DelayedQuitState(controller, 0f, null));
        }
    }

    private class LoosedState : State
    {
        public LoosedState(PlayerController controller) : base(controller)
        { }

        public override void OnEnter(State oldState)
        {
            base.OnEnter(oldState);

            controller.animController.SetDead();
        }

        public override void Update(bool isTapped, float deltaTime)
        {
            base.Update(isTapped, deltaTime);

            controller.rigidbody2d.AddForce(new Vector2(0f, -controller.downForce * deltaTime));

            if (controller.rigidbody2d.velocity.sqrMagnitude < 0.001f)
            {
                controller.SwitchToState(new DelayedQuitState(controller, controller.delayBeforeExitOnLoose, null));
            }
        }
    }

    private class DelayedQuitState : DelayedTransitionState
    {
        private string sceneToLoad;

        public DelayedQuitState(PlayerController controller, float delay, string sceneToLoad) : base(controller, delay)
        {
            this.sceneToLoad = sceneToLoad;
        }

        public override void OnEnter(State oldState)
        {
            base.OnEnter(oldState);
        }

        protected override void MakeTransition()
        {
            Debug.Log("Quiting");
            if (sceneToLoad != null)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}