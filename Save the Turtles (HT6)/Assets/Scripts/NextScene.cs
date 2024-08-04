using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

    public static List<int> trashCan;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        GameObject obj = col.gameObject;
        if (obj.CompareTag("Player")) {
            print(NextSceneInfo.trashCan.Count);
            SceneManager.LoadScene("Sorting Game");
        }
    }
}
