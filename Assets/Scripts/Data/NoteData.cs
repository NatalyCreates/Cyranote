using System.Collections.Generic;
using System.Text.RegularExpressions;

[System.Serializable]
public class NoteData {
    
    public Enums.Character author;
    public string content;
    public List<MadlibData> madlibs;

    public NoteData(Enums.Character author, string content, List<MadlibData> madlibs) {
        this.author = author;
        this.content = content;
        this.madlibs = madlibs;
    }
}
