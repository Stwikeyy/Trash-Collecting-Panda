using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : MonoBehaviour {

    public Sprite[] spriteArray;    
    public GameObject trash;
    
    // Start is called before the first frame update
    void Start() {
        //print(NextSceneInfo.num);
        for (int i = 0; i < NextSceneInfo.trashCan.Count; i++) {
            float xpos = Random.Range(-8, 5), ypos = Random.Range(3, 4);
            GameObject trashClone = Instantiate(trash, new Vector3(xpos, ypos, -1), trash.transform.rotation);
            int trashType = NextSceneInfo.trashCan[i];
            trashClone.GetComponent<SpriteRenderer>().sprite = spriteArray[trashType];
            trashClone.GetComponent<SortingTrash>().trashNum = trashType;
            trashClone.layer = 0;
        }
        NextSceneInfo.trashCan.Clear();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
