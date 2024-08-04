using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            if (player.GetComponent<SortingPlayer>().sortedCorrect >= 50) {
                SceneManager.LoadScene("Ending Scene");
            }
            else {
                SceneManager.LoadScene("Player Scene");
            }
        }
    }
}
