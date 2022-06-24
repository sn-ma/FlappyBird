using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
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
    private Animator animator;

    private State state = null;

    private bool isCollided = false;
    private string triggeredWith = null;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SwitchToState(new PlayingState(this));
    }


    void Update()
    {
        state.Update(Input.touchCount > 0, Time.deltaTime);
        SetRotationByVelocity();
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
        triggeredWith = collision.tag;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCollided = true;
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
            if (controller.isCollided)
            {
                controller.rigidbody2d.velocity = Vector2.zero;
                controller.rigidbody2d.angularVelocity = 0f;
                controller.SwitchToState(new LoosedState(controller));
                return;
            }

            if (controller.triggeredWith != null)
            {
                switch (controller.triggeredWith)
                {
                    case "Obstacle":
                        controller.SwitchToState(new LoosedState(controller));
                        return;
                    case "Finish":
                        controller.SwitchToState(new WinnedState(controller));
                        return;
                    default:
                        Debug.LogError($"Unexpected trigger tag: {controller.triggeredWith}");
                        break;
                }
            }

            if (isTapped)
            {
                controller.rigidbody2d.AddForce(new Vector2(0f, controller.upForce * deltaTime));
                controller.animator.SetBool("isFlying", true);
            }
            else
            {
                controller.rigidbody2d.AddForce(new Vector2(0f, -controller.downForce * deltaTime));
                controller.animator.SetBool("isFlying", false);
            }
            controller.rigidbody2d.velocity = new Vector2(controller.xVelocity, controller.rigidbody2d.velocity.y);
        }

        public override void OnExit(State newState)
        {
            controller.animator.SetBool("isFlying", false);
        }
    }

    private class WinnedState : State
    {
        public WinnedState(PlayerController controller) : base(controller)
        { }

        public override void OnEnter(State oldState)
        {
            controller.rigidbody2d.rotation = 0f;
            controller.rigidbody2d.velocity = new Vector2(controller.xVelocity, 0f);

            Camera.main.GetComponent<FollowXAxis>().enabled = false;
            controller.SwitchToState(new DelayAndQuitState(controller, controller.delayBeforeExitOnWin, null));

            controller.animator.SetBool("isFlying", true);
        }
    }

    private class LoosedState : State
    {
        public LoosedState(PlayerController controller) : base(controller)
        { }

        public override void OnEnter(State oldState)
        {
            controller.animator.SetTrigger("die");
        }

        public override void Update(bool isTapped, float deltaTime)
        {
            controller.rigidbody2d.AddForce(new Vector2(0f, -controller.downForce * deltaTime));

            if (controller.isCollided)
            {
                controller.SwitchToState(new DelayAndQuitState(controller, controller.delayBeforeExitOnLoose, null));
            }
        }
    }

    private class DelayAndQuitState : State
    {
        private float timeLeft;
        private string sceneToLoad;

        public DelayAndQuitState(PlayerController controller, float time, string sceneToLoad) : base(controller)
        {
            this.timeLeft = time;
            this.sceneToLoad = sceneToLoad;
        }

        public override void Update(bool isTapped, float deltaTime)
        {
            timeLeft -= deltaTime;
            if (timeLeft <= 0f)
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
}