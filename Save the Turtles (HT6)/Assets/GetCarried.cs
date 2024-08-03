using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCarried : MonoBehaviour
{
    public Transform targetParent; // Drag the parent object here in the Inspector
    public Vector2 offset; // Set the desired offset in the Inspector
    public Vector2 prevParentPos;

    void Start() {

    }

    void Update()
    {
        // if (targetParent != null)
        // {
        //     prevParentPos
        //     // Set the child object's position relative to its parent
        //     transform.position = targetParent.position + offset;
        // }
    }
}