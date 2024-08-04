using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour {

    public GameObject player;
    public int trashNum;
    public GameObject trashGenerator;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().playerMaxCapacity != collision.gameObject.GetComponent<PlayerController>().trashCount) {
            List<int> lst = player.GetComponent<PlayerController>().trashes;
            lst.Add(trashNum);
            trashGenerator.GetComponent<TrashGenerator>().trashGenerated--;
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player")) print(collision.gameObject.GetComponent<PlayerController>().playerMaxCapacity + " " + collision.gameObject.GetComponent<PlayerController>().trashCount);
    }
}
