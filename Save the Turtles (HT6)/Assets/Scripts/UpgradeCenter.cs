using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCenter : MonoBehaviour {

    public int curPoints;
    public int[] requirements;
    public int[] upgrades;
    private int r = 1;
    private int maxCap = 1;
    public GameObject trashGenerator;
    public Text currentlyCarrying;
    public Text maxCapacity;
    public Text pointsToNextUpgrade;

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
            scr.trashes.Clear();
            currentlyCarrying.text = "Currently Carrying: 0";
            int tmp = r - curPoints;
            pointsToNextUpgrade.text = "Points to Next Upgrade: " + tmp.ToString();
            while (curPoints >= r) {
                curPoints -= r;
                r *= 2;
                maxCap++;
                maxCapacity.text = "Max Capacity: " + maxCap.ToString();
                tmp = r - curPoints;
                pointsToNextUpgrade.text = "Points to Next Upgrade: " + tmp.ToString();
                scr.playerMaxCapacity = maxCap;
            }
        }
    }
}
