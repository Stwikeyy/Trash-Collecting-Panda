using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingGame : MonoBehaviour
{
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;

    float fishTimer;
    [SerializeField] float timerMultiplicator = 1f;

    float fishSpeed;
    [SerializeField] float smoothMotion = 1f;

    [SerializeField] Transform hook;
    float hookPosition;
    float progress;
    //[SerializeField] float progressGoal = 1f;
    [SerializeField] float progressPower = 1f;
    float hookPullVelocity;
    [SerializeField] float hookForce = 1f;
    [SerializeField] float hookGravityPower = 1f;
    [SerializeField] float progressDegradationPower = 1f;

    [SerializeField] Transform progressBar;

    public bool paused = true;
    private SpriteRenderer objectRenderer;
    public bool caught = false;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        if (!paused) {
            Fish();
            HookPhysics();
            HookJump();
            HookUpdate();
            ProgressCheck();
        }
    }

    public bool isCaught() {
        return caught;
    }
    public void unCatch() {
        caught = false;
    }

    public void unpause() {
        //objectRenderer.enabled = true;
        paused = false;
    }

    public void pause() {
        //objectRenderer.enabled = false;
        paused = true;
    }

    void ProgressCheck() {
        Vector3 ls = progressBar.localScale;
        ls.y = progress;
        progressBar.localScale = ls;

        if (progress < 2 && Mathf.Abs(hook.position.y - fish.position.y) < 1) {
            progress += progressPower * Time.deltaTime;
        } else if (progress > 0) {
            progress -= progressDegradationPower * Time.deltaTime;
        }

        if (progress > 2) {
            caught = true;
            progress = 0;
            ls.y = progress;
            progressBar.localScale = ls;
            pause();
        }
    }

    void HookUpdate() {
        hook.Translate(new Vector3(0, hookPullVelocity, 0) * Time.deltaTime);
    }

    void HookJump() {
        if (Input.GetButtonDown("Jump")) {
            hookPullVelocity = hookForce;
        }
    }

    void HookPhysics() {
        hookPullVelocity -= hookGravityPower;
        if (hook.position.y < bottomPivot.position.y) {
            hookPullVelocity = 0;
            hook.position = new Vector3(hook.position.x, bottomPivot.position.y, hook.position.z); // ensure position
        } else if (hook.position.y > topPivot.position.y) {
            hookPullVelocity = 0;
            hook.position = new Vector3(hook.position.x, topPivot.position.y, hook.position.z); // ensure position
        }
    }

    void Fish()
    {
        // shows up when fishing

        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f) {
            fishTimer = UnityEngine.Random.value * timerMultiplicator;
            fishDestination = UnityEngine.Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector2.Lerp(bottomPivot.position, topPivot.position, fishPosition);
        print(fish.position.y);
    }
}
