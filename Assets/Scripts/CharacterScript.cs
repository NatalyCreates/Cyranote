using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterScript : MonoBehaviour
{
    public Sprite[] Sprites;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMood(int score)
    {
        if (score < 0)
            GetComponent<Image>().sprite = Sprites[2];
        if (score == 0)
            GetComponent<Image>().sprite = Sprites[0];
        if (score > 0)
            GetComponent<Image>().sprite = Sprites[1];
    }
}
