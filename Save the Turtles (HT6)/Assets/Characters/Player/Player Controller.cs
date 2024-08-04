using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    // for movement
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    public int trashCount = 0;

    // for fishing
    public int state = 0; // 0: not fishing, 1: fishing, 2: on the line
    public float coastline;
    private float fishingTime = 0;
    private float waitTime = 0;
    public FishingGame fishComponent;
    private int fishCount = 0;

    // for sprite rendering
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    private int curstate = 0;
    private int curSprite = 0;
    private int waiting = 0;

    // for trashCapacity
    public List<int> trashes;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer.sprite = newSprite;

    }

    private void Update() {
        updateSpeed();
        waiting++;
        if (waiting == 50) {
            updateSprite();
            waiting = 0;
        }
        // fishing mode
        if (transform.position.x >= coastline) {
            if (state == 0) { // First instance, throw out the line
                print("Throw");
                fishComponent.pause();
                fishingTime = 0; // Reset fishing time
                state = 1; // Start fishing
                waitTime = 1 + UnityEngine.Random.Range(0.0f, 4.0f); // Random wait time between 1-5 seconds
            } else if (state == 1) { // Fishing
                fishComponent.pause();
                fishingTime += Time.deltaTime; // Increment fishing time
                print(fishingTime);
                print("Fishing");
            } else if (state == 2) { // on the line (catching)
                fishComponent.unpause();
                print("catching");
            }
            if (fishingTime >= waitTime) { // If enough time has passed, fish is caught
                    state = 2;
            }
            if (state == 2 && fishComponent.isCaught()) {
                print("Caught");
                ++fishCount;
                fishComponent.unCatch();
                state = 0;
            }
        } else { // Player is not in fishing mode
            state = 0;
            fishComponent.pause();
        }
    }

    void updateSpeed() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        
        rb2d.velocity = moveInput * (moveSpeed - trashCount);

        // 0 right, 1 left, 2 down, 3 up, 4 is idle
        if (rb2d.velocity.x > 0) curstate = 0;
        else if (rb2d.velocity.x < 0) curstate = 1;
        else if (rb2d.velocity.y < 0) curstate = 2;
        else if (rb2d.velocity.y > 0) curstate = 3;
        else curstate = 4;
    }

    void updateSprite() {
        // 0 right, 1 left, 2 down, 3 up, 4 is idle
        if (curstate == 0 || curstate == 1) {
            if (curSprite >= 8 && curSprite <= 11) {
                curSprite++;
                if (curSprite > 11) curSprite = 8;
            }
            else curSprite = 8;
            if (curstate == 0 && spriteRenderer.flipX) spriteRenderer.flipX = false;
            if (curstate == 1 && !spriteRenderer.flipX) spriteRenderer.flipX = true;
        }
        else if (curstate == 2) {
            if (curSprite >= 4 && curSprite <= 7) {
                curSprite++;
                if (curSprite > 7) curSprite = 4;
            }
            else curSprite = 4;
        }
        else if (curstate == 3) {
            if (curSprite >= 0 && curSprite <= 3) {
                curSprite++;
                if (curSprite > 3) curSprite = 0;
            }
            else curSprite = 0;
        }
        else {
            if (curSprite >= 12 && curSprite <= 14) {
                curSprite++;
                if (curSprite > 14) curSprite = 12;
            }
            else curSprite = 12;
        }
        spriteRenderer.sprite = spriteArray[curSprite];
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Item")) {
            ++trashCount;
            //trashes.Add(collision.gameObject.getComponent<TrashController>().trashType);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("GarbageBasket")) trashCount = 0;
        if (col.gameObject.CompareTag("UpgradeCenter")) trashCount = 0;
        //for (int i = 0; i < trashes.Count; i++) print(trashes[i]);
    }
}
