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
    public float coastline;
    private float fishingTime = 0;
    private float waitTime = 0;


    // for sprite rendering
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    // for trashCapacity
    public List<int> trashes;

    // Start is called before the first frame update
    void Start() {
        spriteRenderer.sprite = newSprite;
    }

    private void Update() {
        updateSpeed();

        // fishing mode
        if (transform.position.x >= coastline) {
            if (!fishing) { // first instance, throw out line
                fishingTime = 0;
                fishing = true;
                waitTime = 1 + UnityEngine.Random.Range(0.0f, 4.0f); // between 1-5 seconds
            } else { // fishing for fish
                fishingTime += Time.deltaTime;
                if (fishingTime >= waitTime) { // fish is being catched
                    fishing = false;
                    print("FISH!");
                }
            }
        } else {
            fishing = false;
        } // not in fishing mode
    }

    void updateSpeed() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        
        rb2d.velocity = moveInput * (moveSpeed - trashCount);

        if (rb2d.velocity.x > 0) {
            if (spriteRenderer.flipX) spriteRenderer.flipX = false;
            spriteRenderer.sprite = spriteArray[1];
        }
        else if (rb2d.velocity.x < 0) {
            if (!spriteRenderer.flipX) spriteRenderer.flipX = true;
            spriteRenderer.sprite = spriteArray[1];
        }
        else if (rb2d.velocity.y > 0) {
            spriteRenderer.sprite = spriteArray[0];
        }
        else {
            spriteRenderer.sprite = spriteArray[2];
        }

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Item")) ++trashCount;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("GarbageBasket")) trashCount = 0;
        if (col.gameObject.CompareTag("UpgradeCenter")) trashCount = 0;
        //for (int i = 0; i < trashes.Count; i++) print(trashes[i]);
    }
}
