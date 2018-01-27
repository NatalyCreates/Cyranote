using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private StoryData story;
    public NoteData currentNoteData;
    private int gameScore;
    private int nextNoteId;
    public static GameManager Instance;

    private bool introShowing = true;

    [SerializeField] GameObject NotePre;
    [SerializeField] GameObject ClassObj;

    [SerializeField] GameObject rightBtn;
    [SerializeField] GameObject leftBtn;

    [SerializeField] CharacterScript rightCharacter;
    [SerializeField] CharacterScript leftCharacter;

    GameObject CurrNote;
    [SerializeField] GameObject introPanel;

    public void Awake() {
        if (Instance == null) Instance = this;
        else throw new System.Exception("GameManager class is Singleton, but has more than 1 instance!");
    }

    public void Start() {
        story = DataLoader.LoadStory();
        gameScore = 0;
        currentNoteData = story.notes[0];
        nextNoteId = currentNoteData.noteId;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (introShowing)
            {
                introPanel.SetActive(false);
                introShowing = false;
            }

            Animator anim = ClassObj.GetComponent<Animator>();

            anim.SetBool("right", false);
            anim.SetBool("left", false);
        }
    }

    public NoteData GetNewNote() {
        return currentNoteData;
    }

    void SetButtons(Enums.Character character)
    {
        if (character == Enums.Character.Beth)
        {
            rightBtn.active = true;
            leftBtn.active = false;
        }
        else
        {
            rightBtn.active = false;
            leftBtn.active = true;
        }
    }

    public void BtnSend(bool isRight)
    {
        List<OptionData> selectedList = CurrNote.GetComponent<NoteCreator>().GetSelected();
        int noteScore = CalculateTotal(selectedList);
        gameScore += noteScore;

        Destroy(CurrNote);
        rightBtn.active = false;
        leftBtn.active = false;

        if (!currentNoteData.skipCutToCharacter)
        {
            if (isRight)
            {
                rightCharacter.SetMood(noteScore);
                ClassObj.GetComponent<Animator>().SetBool("right", true);
            }
            else
            {
                leftCharacter.SetMood(noteScore);
                ClassObj.GetComponent<Animator>().SetBool("left", true);
            }
        }

        nextNoteId = GetNextNoteId(selectedList);
        currentNoteData = story.notes.Where(n => n.noteId == nextNoteId).Select(n => n).ToList()[0];
    }

    int CalculateTotal(List<OptionData> selectedList)
    {
        int sum = 0;

        foreach(OptionData selected in selectedList)
        {
            sum += selected.scoreEffect;
        }

        return sum;
    }

    int GetNextNoteId(List<OptionData> selectedList)
    {
        int NextNoteId = currentNoteData.nextNoteId;
        Debug.Log("NextNoteId = " + NextNoteId);

        foreach (OptionData selected in selectedList)
        {
            if (selected.nextNoteId != -1)
            {
                NextNoteId = selected.nextNoteId;
            }
        }

        Debug.Log("NextNoteId = " + NextNoteId);
        return NextNoteId;
    }

    public void CreateNewNote()
    {
        CurrNote = Instantiate(NotePre, ClassObj.transform);
        SetButtons(currentNoteData.author);
    }
}
