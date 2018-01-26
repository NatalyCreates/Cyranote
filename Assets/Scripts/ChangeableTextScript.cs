using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeableTextScript : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] List<string> options;

    string selected;
    int count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = selected;

    }

    public void InitOptions(List<string> optionslist)
    {
        options = optionslist;

        selected = options[0];
    }

    public void BtnUp()
    {
        count--;

        if (count < 0)
            count = options.Count - 1;

        selected = options[count];
    }

    public void BtnDown()
    {
        count++;

        if (count > options.Count - 1)
            count = 0;

        selected = options[count];
    }
}
