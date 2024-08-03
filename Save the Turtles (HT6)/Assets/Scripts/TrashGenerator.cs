using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour {
    
    public GameObject trash;
    public int num;
    public int trashGenerated;

    // Start is called before the first frame update
    

    void Start() {
        createTrash(10);
    }

    void Update() {
        if (num < 1000) num++;
        if (num >= 1000 && trashGenerated <= 15) {
            createTrash(1);
            num = 0;
        }
    }

    void createTrash(int num) {
        for (int i = 0; i < num; i++) {
            int xpos = Random.Range(-10, 10), ypos = Random.Range(-5, 5);
            GameObject trashClone = Instantiate(trash, new Vector3(xpos, ypos, 0), trash.transform.rotation);
            trashClone.layer = 0;
            trashGenerated++;
        }
    }
}
