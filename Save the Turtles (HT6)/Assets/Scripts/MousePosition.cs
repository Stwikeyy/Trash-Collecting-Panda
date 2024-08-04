using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = Input.mousePosition;
        transform.position = cam.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //print(transform.position.x + " " + transform.position.y);
        
    }

    void OnTriggerStay2D(Collider2D col) {
        if (Input.GetMouseButtonDown(0)) SceneManager.LoadScene("Sorting Game");
    }
}
