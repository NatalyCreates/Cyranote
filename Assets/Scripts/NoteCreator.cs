using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    [SerializeField] GameObject normalTextPre;
    [SerializeField] GameObject changeableTextPre;

    NoteData data;

	// Use this for initialization
	void Start () {
        data = GameManager.Instance.GetNewNote();

        string[] contentStructure = data.content.Split(';');

        foreach(string contentPart in contentStructure)
        {
            if (contentPart == "$")
            {

            }
            else
            {
                GameObject textObj = Instantiate(normalTextPre, new Vector3(0, 0, 0), Quaternion.identity, transform);
                textObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                textObj.GetComponent<Text>().text = contentPart;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
