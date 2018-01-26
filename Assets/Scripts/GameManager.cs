using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public void Start() {
        StoryData s = DataLoader.LoadStory();
        foreach (NoteData n in s.notes)
        {
            Debug.Log(n.content);
            string note = "";
            string[] parts = n.content.Split('$');
            List<string> noteParts = new List<string>();

            foreach (string a in parts)
            {
                Debug.Log(a);
            }
        }

    }

}
