using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private StoryData story;
    private int gameScore;
    private int nextNoteId;
    public static GameManager Instance;

    [SerializeField] GameObject NotePre;
    [SerializeField] GameObject ClassObj;

    [SerializeField] GameObject rightBtn;
    [SerializeField] GameObject leftBtn;

    [SerializeField] CharacterScript rightCharacter;
    [SerializeField] CharacterScript leftCharacter;

    GameObject CurrNote;

    public void Awake() {
        if (Instance == null) Instance = this;
        else throw new System.Exception("GameManager class is Singleton, but has more than 1 instance!");
    }

    public void Start() {
        story = DataLoader.LoadStory();
        gameScore = 0;
        nextNoteId = 0;

        CreateNewNote();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Animator anim = ClassObj.GetComponent<Animator>();

            if (anim.GetBool("right") || anim.GetBool("left"))
            {
                Destroy(CurrNote);
                rightBtn.active = false;
                leftBtn.active = false;
            }

            anim.SetBool("right", false);
            anim.SetBool("left", false);
        }
    }

    public NoteData GetNewNote() {
        return story.notes[nextNoteId];
    }

    void SetButtons(Enums.Character character)
    {
        if (character == Enums.Character.Right)
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
        int NextNoteId = 0;

        foreach (OptionData selected in selectedList)
        {
            if (selected.nextNoteId != -1)
            {
                NextNoteId = selected.nextNoteId;
            }
        }

        return NextNoteId;
    }

    public void CreateNewNote()
    {
        CurrNote = Instantiate(NotePre, ClassObj.transform);
        SetButtons(CurrNote.GetComponent<NoteCreator>().data.author);
    }
}
