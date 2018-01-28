using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontChooser : MonoBehaviour {

    public Font AllieFont, BethFont;

    void Start () {
        // Allie
        if (GameManager.Instance.currentNoteData.author == 0)
        {
            GetComponent<Text>().font = AllieFont;
        }
        else
        {
            GetComponent<Text>().font = BethFont;
            GetComponent<Text>().fontStyle = FontStyle.Bold;
        }
	}
}
