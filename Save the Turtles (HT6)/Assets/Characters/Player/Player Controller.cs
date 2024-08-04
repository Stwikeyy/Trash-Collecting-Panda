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
    public bool fishing = false;
    public bool onTheLine = false;
    public float coastline;
    private float fishingTime = 0;
    private float waitTime = 0;
    public FishingGame fishComponent;

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
            if (!fishing) { // First instance, throw out the line
            print("Throw");
                if (!onTheLine) { // Only start fishing if the fish isn't on the line
                    fishingTime = 0; // Reset fishing time
                    fishing = true; // Start fishing
                    fishComponent.unCatch(); // remove previous catch
                    waitTime = 1 + UnityEngine.Random.Range(0.0f, 4.0f); // Random wait time between 1-5 seconds
                }
            } else { // Fishing for fish on the line
                fishingTime += Time.deltaTime; // Increment fishing time
                print("Fishing");
                if (fishingTime >= waitTime) { // If enough time has passed, fish is caught
                    fishing = false; // Stop fishing
                    onTheLine = true; // The fish is now on the line
                    print("On the Line");
                    if (fishComponent != null) {
                        fishComponent.unpause(); // Resume fish behavior
                    }
                }
            }
            // Check if the fish is caught
            if (fishComponent != null && fishComponent.isCaught()) {
                print("Caught");
                onTheLine = false; // The fish is no longer on the line
                fishing = false;
                fishComponent.pause(); // Pause fish behavior
            }
        } else { // Player is not in fishing mode
            fishing = false;
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
