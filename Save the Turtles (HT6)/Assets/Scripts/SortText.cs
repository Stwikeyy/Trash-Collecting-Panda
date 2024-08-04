using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortText : MonoBehaviour
{
    // Start is called before the first frame update
    public int sortedCorrect = 0;
    public int needCorrect = 0;
    public Text sortedCorrectText;
    public Text needCorrectText;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        //sortedCorrectText.text = "Sorted Correctly: " + sortedCorrect;
        //needCorrectText.text = "Target: " + needCorrect;
    }
}
