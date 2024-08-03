using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrashGenerator : MonoBehaviour {
    
    public GameObject trash;
    public int num;
    public int trashGenerated;

    public Sprite[] sprites;
    public GameObject grass;

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
            int counter = 0;
            while (true) {
                xpos = Random.Range(-10, 10);
                ypos = Random.Range(-5, 5);
                counter++;
                Tilemap collide = grass.GetComponent<Tilemap>();
                if (counter == 10) print("hayday");
                if (collide.HasTile(new Vector3Int(xpos, ypos, 0)) || counter == 10) {
                    break;
                }
            }
            GameObject trashClone = Instantiate(trash, new Vector3(xpos, ypos, -1), trash.transform.rotation);
            int trashType = Random.Range(0, 6);
            trashClone.GetComponent<SpriteRenderer>().sprite = sprites[trashType];
            trashClone.layer = 0;
            trashGenerated++;
        }
    }
}
