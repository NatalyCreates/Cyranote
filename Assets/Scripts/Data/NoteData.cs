using System.Collections.Generic;
using System.Text.RegularExpressions;

[System.Serializable]
public class NoteData {

    public int noteId;
    public Enums.Character author;
    public string content;
    public bool triggersEndingCheck;
    public bool skipCutToCharacter;
    public int nextNoteId;
    public List<MadlibData> madlibs;

    public NoteData(int noteId, Enums.Character author, string content, bool triggersEndingCheck, bool skipCutToCharacter, int nextNoteId, List<MadlibData> madlibs) {
        this.noteId = noteId;
        this.author = author;
        this.content = content;
        this.triggersEndingCheck = triggersEndingCheck;
        this.skipCutToCharacter = skipCutToCharacter;
        this.nextNoteId = nextNoteId;
        this.madlibs = madlibs;
    }
}
