﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private int FIRST_NOTE_ID = 0;
    private int GAME_OVER_NOTE_ID = -500;
    private int END_STORY_NOTE_ID = 5000;

    private float INSTRUCTIONS_READING_TIME = 2f;

    private StoryData story;
    public NoteData currentNoteData;

    private int gameScore;
    private int nextNoteId;
    
    private bool introShowing = true;

    // Intro
    [SerializeField] GameObject introPanel;
    [SerializeField] GameObject introInstruction;

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

	[SerializeField] GameObject miniGameUi;
	[SerializeField] GameObject miniGame;
	[SerializeField] GameObject phoneUi;

    // Anim Arms
    public Animator arms;

    private GameObject currentNoteObj;

    public void Awake() {
        if (Instance == null) Instance = this;
        else throw new System.Exception("GameManager class is Singleton, but has more than 1 instance!");
    }

    public void Start() {

        allieAvatar.SetActive(false);
        bethAvatar.SetActive(false);
        sendToAllieButton.SetActive(false);
        sendToBethButton.SetActive(false);

        story = DataLoader.LoadStory();
        gameScore = 0;
        currentNoteData = story.notes.Where(noteData => noteData.noteId == FIRST_NOTE_ID).Select(noteData => noteData).ToList()[0];
        nextNoteId = currentNoteData.nextNoteId;
        Debug.Log("Current Note Id = " + currentNoteData.noteId);

        StartCoroutine(ShowInstruction());
    }
    
    private IEnumerator ShowInstruction() {
        yield return new WaitForSeconds(INSTRUCTIONS_READING_TIME);
        introInstruction.SetActive(true);
    }

    public IEnumerator StartNoteAnim() {
        yield return new WaitForSeconds(0.4f);
        arms.SetInteger("SendNote", 0);
        yield return new WaitForSeconds(0.4f);
        if (currentNoteData.author == Enums.Character.Allie)
        {
            arms.SetInteger("ReceiveNote", 1);
            // animator then calls the event to open the note
        }
        else
        {
            arms.SetInteger("ReceiveNote", -1);
            // animator then calls the event to open the note
        }
    }
    
    public void OpenNote() {
        arms.SetInteger("ReceiveNote", 0);

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
        if (Input.GetMouseButtonDown(0) && Time.timeSinceLevelLoad > INSTRUCTIONS_READING_TIME)
        {
            if (introShowing)
            {
				phoneUi.SetActive (true);
                introPanel.SetActive(false);
                introShowing = false;
                StartCoroutine(StartNoteAnim());
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("OpeningScreen");
        }
    }

    void UpdateSendingButtons()
    {
        // Game end - no buttons to send
        if (nextNoteId == GAME_OVER_NOTE_ID)
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
        int noteScore;

        if (selectedOptions.Count == 0) noteScore = currentNoteData.scoreValue;
        else
        {
            noteScore = selectedOptions.Sum(opt => opt.scoreEffect);
        }

        gameScore += noteScore;
        Debug.Log("Game Score: " + gameScore);

        Destroy(currentNoteObj);

        sendToAllieButton.SetActive(false);
        sendToBethButton.SetActive(false);

        if (currentNoteData.author == Enums.Character.Allie)
        {
            arms.SetInteger("SendNote", -1);
        }
        else
        {
            arms.SetInteger("SendNote", 1);
        }

        OverrideNextNoteId(selectedOptions);
        Debug.Log("Next Note Id = " + nextNoteId);
        
        if (nextNoteId == 5000)
        {
            Debug.Log("GO TO END");
            if (gameScore <= 0) nextNoteId = 1360;
            else nextNoteId = 2360;
        }
        StartCoroutine(ShowAvatar(noteScore));
    }

    IEnumerator ShowAvatar(int noteScore) {
        yield return new WaitForSeconds(0.4f);
        AudioManager.Instance.PlaySound(Enums.SoundType.Paper);
        yield return new WaitForSeconds(0.6f);
        if (currentNoteData.author == Enums.Character.Allie)
        {
            bethAvatar.SetActive(true);
            bethAvatar.GetComponent<CharacterScript>().SetMood(noteScore);
            if (noteScore > 0) AudioManager.Instance.PlaySound(Enums.SoundType.GoodB);
            else if (noteScore == 0) AudioManager.Instance.PlaySound(Enums.SoundType.NormalB);
            else if (noteScore < 0) AudioManager.Instance.PlaySound(Enums.SoundType.BadB);
        }
        else
        {
            allieAvatar.SetActive(true);
            allieAvatar.GetComponent<CharacterScript>().SetMood(noteScore);
            if (noteScore > 0) AudioManager.Instance.PlaySound(Enums.SoundType.GoodA);
            else if (noteScore == 0) AudioManager.Instance.PlaySound(Enums.SoundType.NormalA);
            else if (noteScore < 0) AudioManager.Instance.PlaySound(Enums.SoundType.BadA);
        }
        yield return new WaitForSeconds(0.5f);
        currentNoteData = story.notes.Where(noteData => noteData.noteId == nextNoteId).Select(noteData => noteData).ToList()[0];
        Debug.Log("Current Note Id = " + currentNoteData.noteId);
        nextNoteId = currentNoteData.nextNoteId;
        StartCoroutine(StartNoteAnim());
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

	public void phoneBtn()
	{
		miniGame.SetActive (true);
		miniGameUi.SetActive (true);
	}

	public void phoneBtnEsc()
	{
		miniGame.SetActive (false);
		miniGameUi.SetActive (false);
	}
}
