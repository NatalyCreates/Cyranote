using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private int FIRST_NOTE_ID = 140;
    private int END_NOTE_ID = 5000;
    private int CREDITS_NOTE = 9999;

    private StoryData story;
    public NoteData currentNoteData;

    private int gameScore;
    private int nextNoteId;
    
    private bool introShowing = true;

    // Intro
    [SerializeField] GameObject introPanel;

    // Notes
    [SerializeField] GameObject noteAlliePrefab;
    [SerializeField] GameObject noteBethPrefab;
    [SerializeField] GameObject noteParent;
    
    // Buttons
    [SerializeField] GameObject sendToAllieButton;
    [SerializeField] GameObject sendToBethButton;

    // Avatars
    [SerializeField] GameObject allieAvatar;
    [SerializeField] GameObject bethAvatar;

    private GameObject currentNoteObj;

    public void Awake() {
        if (Instance == null) Instance = this;
        else throw new System.Exception("GameManager class is Singleton, but has more than 1 instance!");
    }

    public void Start() {

        story = DataLoader.LoadStory();
        gameScore = 0;
        currentNoteData = story.notes.Where(noteData => noteData.noteId == FIRST_NOTE_ID).Select(noteData => noteData).ToList()[0];
        nextNoteId = currentNoteData.nextNoteId;
        Debug.Log("Current Note Id = " + currentNoteData.noteId);
        
        OpenNote();
    }
    
    public void StartNoteAnim() {
        if (currentNoteData.author == Enums.Character.Allie)
        {
            // set anim value to 1
            // animator then calls the event to open the note
        }
        else
        {
            // set anim value to -1
            // animator then calls the event to open the note
        }
    }
    
    public void OpenNote() {

        allieAvatar.SetActive(false);
        bethAvatar.SetActive(false);
        sendToAllieButton.SetActive(false);
        sendToBethButton.SetActive(false);

        UpdateSendingButtons();
        if (currentNoteData.author == Enums.Character.Allie)
        {
            currentNoteObj = Instantiate(noteAlliePrefab, noteParent.transform);
        }
        else
        {
            currentNoteObj = Instantiate(noteBethPrefab, noteParent.transform);
        }
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
        }
    }

    void UpdateSendingButtons()
    {
        // Game end - no buttons to send
        if (nextNoteId == END_NOTE_ID)
        {
            sendToAllieButton.SetActive(false);
            sendToBethButton.SetActive(false);
            Debug.Log("GAME END");
        }
        else
        {
            if (currentNoteData.author == Enums.Character.Allie)
            {
                sendToAllieButton.SetActive(false);
                sendToBethButton.SetActive(true);
            }
            else
            {
                sendToAllieButton.SetActive(true);
                sendToBethButton.SetActive(false);
            }
        }
    }
    
    public void SendNote()
    {
        List<OptionData> selectedOptions = currentNoteObj.GetComponent<NoteCreator>().GetSelected();
        int noteScore = CalcScoreFromSelectedOptions(selectedOptions);
        Debug.Log("Note Score was: " + noteScore);
        noteScore = selectedOptions.Sum(opt => opt.scoreEffect);
        Debug.Log("Note Score was: " + noteScore);
        gameScore += noteScore;

        Destroy(currentNoteObj);

        sendToAllieButton.SetActive(false);
        sendToBethButton.SetActive(false);

        if (currentNoteData.author == Enums.Character.Allie)
        {
            bethAvatar.SetActive(true);
            bethAvatar.GetComponent<CharacterScript>().SetMood(noteScore);
        }
        else
        {
            allieAvatar.SetActive(true);
            allieAvatar.GetComponent<CharacterScript>().SetMood(noteScore);
        }

        OverrideNextNoteId(selectedOptions);
        Debug.Log("Next Note Id = " + nextNoteId);
        
        if (nextNoteId == END_NOTE_ID)
        {
            Debug.Log("GAME END");
        }
        else
        {
            currentNoteData = story.notes.Where(noteData => noteData.noteId == nextNoteId).Select(noteData => noteData).ToList()[0];
            Debug.Log("Current Note Id = " + currentNoteData.noteId);
            nextNoteId = currentNoteData.nextNoteId;
            OpenNote();
        }
    }

    int CalcScoreFromSelectedOptions(List<OptionData> selectedOptions)
    {
        int sum = 0;
        foreach(OptionData opt in selectedOptions)
        {
            sum += opt.scoreEffect;
        }
        return sum;
    }
    
    void OverrideNextNoteId(List<OptionData> selectedOptions)
    {
        nextNoteId = currentNoteData.nextNoteId;
        foreach (OptionData opt in selectedOptions)
        {
            if (opt.nextNoteId != -1)
            {
                nextNoteId = opt.nextNoteId;
            }
        }
    }
}
