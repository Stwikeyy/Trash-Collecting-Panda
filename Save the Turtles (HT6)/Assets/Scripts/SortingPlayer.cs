using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SortingPlayer : MonoBehaviour {

    // for movement
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    public int trashCount = 0;

    // for sprite rendering
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    private int curstate = 0;
    private int curSprite = 0;
    private int waiting = 0;

    // for trashCapacity
    public List<int> trashes;
    public bool holdingTrash = false;
    public int holdingTrashNum = 0;

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
    }

    void updateSpeed() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        
        rb2d.velocity = moveInput * moveSpeed;

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
