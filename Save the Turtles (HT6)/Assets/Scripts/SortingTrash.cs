using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingTrash : MonoBehaviour {

    public int trashNum;
    public GameObject player;
    public bool parented;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (parented) transform.position = player.transform.position;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player") && !parented && !player.GetComponent<SortingPlayer>().holdingTrash) {
            parented = true;
            transform.SetParent(player.transform);
            player.GetComponent<SortingPlayer>().holdingTrash = true;
        }
    }
}
