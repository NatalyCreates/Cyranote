﻿using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    private StoryData story;

    public static GameManager Instance;

    public void Awake() {
        if (Instance == null) Instance = this;
        else throw new System.Exception("GameManager class is Singleton, but has more than 1 instance!");
    }

    public void Start() {
        story = DataLoader.LoadStory();
    }

    public NoteData GetNewNote() {
        return story.notes[0];
    }
}