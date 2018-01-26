using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private StoryData story;
    private int gameScore;
    private int nextNoteId;
    public static GameManager Instance;

    [SerializeField] GameObject NotePre;
    [SerializeField] GameObject ClassObj;

    GameObject CurrNote;

    public void Awake() {
        if (Instance == null) Instance = this;
        else throw new System.Exception("GameManager class is Singleton, but has more than 1 instance!");
    }

    public void Start() {
        story = DataLoader.LoadStory();
        gameScore = 0;
        nextNoteId = 0;

        CurrNote = Instantiate(NotePre, ClassObj.transform);
    }

    public NoteData GetNewNote() {
        return story.notes[nextNoteId];
    }

    public void BtnSend(bool isRight)
    {
        int noteScore = CurrNote.GetComponent<NoteCreator>().GetScore();
        gameScore += noteScore;

        if (isRight)
            ClassObj.GetComponent<Animator>().SetBool("right", true);
        else
            ClassObj.GetComponent<Animator>().SetBool("left", true);
    }
}
