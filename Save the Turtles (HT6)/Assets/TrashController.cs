using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour {

    public GameObject player;
    public int trashNum;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            List<int> lst = player.GetComponent<PlayerController>().trashes;
            lst.Add(trashNum);
            Destroy(gameObject);
        }
    }
}
