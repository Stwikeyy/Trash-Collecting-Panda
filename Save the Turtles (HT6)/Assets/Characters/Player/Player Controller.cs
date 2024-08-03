using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    public int trashCount = 0;
    
    public bool fishing = false;
    private bool catching = false;
    public float coastline;
    private float fishingTime = 0;
    private float waitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * (moveSpeed - trashCount);

        // fishing mode
        if (transform.position.x >= coastline) {
            if (!fishing) { // first instance, throw out line
                fishingTime = 0;
                fishing = true;
                waitTime = 1 + UnityEngine.Random.Range(0.0f, 4.0f); // between 1 and 5 seconds

            } else { // fishing for fish
                fishingTime += Time.deltaTime;
                if (fishingTime >= waitTime) { // fish is being catched
                    print("FISH!");
                }
            }
        } else {
            fishing = false;
        } // not in fishing mode
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Item")) {
            ++trashCount;
        }
    }
}
