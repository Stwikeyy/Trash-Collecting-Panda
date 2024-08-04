using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageBasket : MonoBehaviour {

    public int requirement;
    public int curPoints;
    public List<int> curTrash;
    public GameObject trashGenerator;

    public Text currentlyCarrying;
    public Text currentTrashPoints;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("Player")) {
            PlayerController scr = obj.GetComponent<PlayerController>();
            for (int i = 0; i < scr.trashCount; i++) {
                curTrash.Add(scr.trashes[i]);
                NextSceneInfo.trashCan.Add(scr.trashes[i]);
            }
            scr.trashes.Clear();
            curPoints += scr.trashCount;
            scr.trashCount = 0;
            currentlyCarrying.text = "Currently Carrying: 0";
            currentTrashPoints.text = "Current Trash Points: " + curPoints.ToString();
        }
    }
}
