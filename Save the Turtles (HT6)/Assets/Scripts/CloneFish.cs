using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneFish : MonoBehaviour {
    public float changeSpeed;

    // Start is called before the first frame update

    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(transform.position.x + changeSpeed, transform.position.y, transform.position.z); 
        //transform.Rotate(new Vector2(0f, 100f) * Time.deltaTime);
        //transform.Rotate(new Vector3(100f, 0f, 0f) * Time.deltaTime);
        //transform.Rotate(new Vector3(0f, 0f, 100f) * Time.deltaTime);
    }
}
