using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {

    private StoryData story;

    public void Start() {
        story = DataLoader.LoadStory();
    }

    public NoteData GetNewNote() {
        return story.notes[0];
    }
}
