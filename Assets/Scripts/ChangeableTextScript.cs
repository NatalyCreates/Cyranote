using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeableTextScript : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] List<OptionData> options;

    OptionData selected;
    int count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = selected.text;

    }

    public void InitOptions(List<OptionData> optionslist)
    {
        options = optionslist;

        selected = options[0];

        GetComponent<RectTransform>().sizeDelta = new Vector2(26 * GetLongestOption(optionslist), 67);
    }

    int GetLongestOption(List<OptionData> optionslist)
    {
        int max = 0;

        foreach(OptionData option in optionslist)
        {
            if (option.text.Length > max)
                max = option.text.Length;
        }

        return max;
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
