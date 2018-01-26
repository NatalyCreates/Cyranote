using System.Collections.Generic;
using System.Text.RegularExpressions;

[System.Serializable]
public class NoteData {
    
    public Enums.Character author;
    public string content;
    public List<List<OptionData>> options;

    public NoteData(Enums.Character author, string content, List<List<OptionData>> options) {
        this.author = author;
        this.content = content;
        this.options = options;
    }
}
