using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    [SerializeField] GameObject normalTextPre;
    [SerializeField] GameObject changeableTextPre;

    Vector2 writingPos = new Vector2(-1350, 265);
    [SerializeField]float textWidth;
    [SerializeField]float textHeight;

    private NoteData noteData;

    List<ChangeableTextScript> changeableList = new List<ChangeableTextScript>();

	void Start () {
        noteData = GameManager.Instance.currentNoteData;
        string[] contentStructure = noteData.content.Split(';');

        int changeableCount = 0;
        Transform lastTransform = null;

        foreach(string contentPart in contentStructure)
        {
            if (contentPart == "#")
            {
                lastTransform = null;
                writingPos = new Vector2(writingPos.x, writingPos.y - textHeight);
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
                changeableTextObj.GetComponent<ChangeableTextScript>().InitOptions(noteData.madlibs[changeableCount].options);
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
                textObj.GetComponent<RectTransform>().sizeDelta = new Vector2(28 * contentPart.Length, 100);
                lastTransform = textObj.transform;
            }
        }
	}

    public List<OptionData> GetSelected()
    {
        List<OptionData> selectedList = new List<OptionData>();

        foreach (ChangeableTextScript currOption in changeableList)
        {
            selectedList.Add(currOption.selected);
        }

        return selectedList;
    }
}
