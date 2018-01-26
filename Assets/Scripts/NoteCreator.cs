using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    [SerializeField] GameObject normalTextPre;
    [SerializeField] GameObject changeableTextPre;

    Vector2 writingPos = new Vector2(-1511, 191);
    [SerializeField]float textWidth;
    [SerializeField]float textHight;

    NoteData data;

    List<ChangeableTextScript> changeableList = new List<ChangeableTextScript>();

	// Use this for initialization
	void Start () {
        data = GameManager.Instance.GetNewNote();

        string[] contentStructure = data.content.Split(';');

        int changeableCount = 0;
        Transform lastTransform = null;

        foreach(string contentPart in contentStructure)
        {
            if (contentPart == "#")
            {
                lastTransform = null;
                writingPos = new Vector2(writingPos.x, writingPos.y - textHight);
            }
            else if (contentPart == "$")
            {
                GameObject changeableTextObj = null;

                if (lastTransform != null)
                {
                    changeableTextObj = Instantiate(changeableTextPre, new Vector3(0, 0, 0), Quaternion.identity, lastTransform);
                    changeableTextObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    changeableTextObj = Instantiate(changeableTextPre, new Vector3(0, 0, 0), Quaternion.identity, transform);
                    changeableTextObj.GetComponent<RectTransform>().anchoredPosition = writingPos;
                }
                changeableTextObj.GetComponent<ChangeableTextScript>().InitOptions(data.madlibs[changeableCount].options);
                changeableCount++;
                lastTransform = changeableTextObj.transform;
                changeableList.Add(changeableTextObj.GetComponent<ChangeableTextScript>());
            }
            else
            {
                GameObject textObj = null;

                if (lastTransform != null)
                {
                    textObj = Instantiate(normalTextPre, new Vector3(0, 0, 0), Quaternion.identity, lastTransform);
                    textObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    textObj = Instantiate(normalTextPre, new Vector3(0, 0, 0), Quaternion.identity, transform);
                    textObj.GetComponent<RectTransform>().anchoredPosition = writingPos;
                }
                textObj.GetComponent<Text>().text = contentPart;
                textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(25 * contentPart.Length, 67);
                lastTransform = textObj.transform;
            }
        }
	}

    public int GetScore()
    {
        int sum = 0;

        foreach (ChangeableTextScript currOption in changeableList)
        {
            sum += currOption.selected.scoreEffect;
        }

        Debug.Log("sum = " + sum.ToString());
        return sum;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
