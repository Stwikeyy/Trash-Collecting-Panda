using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCenter : MonoBehaviour {

    public int curPoints;
    public int[] requirements;
    public int[] upgrades;
    public GameObject trashGenerator;

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
            curPoints += scr.trashCount;
            scr.trashCount = 0;
        }
    }
}
