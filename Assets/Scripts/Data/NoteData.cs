using System.Collections.Generic;
using System.Text.RegularExpressions;

[System.Serializable]
public class NoteData {

    public int noteId;
    public Enums.Character author;
    public string content;
    public bool triggersEndingCheck;
    public int nextNoteId;
    public List<MadlibData> madlibs;

    public NoteData(int noteId, Enums.Character author, string content, bool triggersEndingCheck, int nextNoteId, List<MadlibData> madlibs) {
        this.noteId = noteId;
        this.author = author;
        this.content = content;
        this.triggersEndingCheck = triggersEndingCheck;
        this.nextNoteId = nextNoteId;
        this.madlibs = madlibs;
    }
}
