using System.Collections.Generic;

[System.Serializable]
public class StoryData {

    public List<NoteData> notes;
    public List<NoteData> endings;

    public StoryData(List<NoteData> notes, List<NoteData> endings) {
        this.notes = notes;
        this.endings = endings;
    }
}
