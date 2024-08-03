using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {
    // Start is called before the first frame update
    
    public GameObject fish;
    

    void Start() {
        createfish(0);
    }

    void Update() {

    }

    // Update is called once per frame
    /*void Update() {
        
    }*/
    void createfish(int fishNum) {
        for (int i = 0; i < fishNum; i++) {
            int curnum = Random.Range(0, 2);
            float yval = Random.Range(0, 5);
            int startval = -8;
            float num = 0.001f;
            if (curnum == 1) {
                num *= -1;
                startval *= -1;
            }
            GameObject fishClone = Instantiate(fish, new Vector3(startval, yval * -1, 0), fish.transform.rotation);
            fishClone.GetComponent<CloneFish>().changeSpeed = num;
            fishClone.layer = 0;
        }
    }
}
