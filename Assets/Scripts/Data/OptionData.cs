using System.Collections.Generic;

[System.Serializable]
public class OptionData {

    public string text;
    public int scoreEffect;
    public Enums.StoryFlavor flavor;
    public int nextNoteId;

    public OptionData(string text, int scoreEffect, Enums.StoryFlavor flavor, int nextNoteId) {
        this.text = text;
        this.scoreEffect = scoreEffect;
        this.flavor = flavor;
        this.nextNoteId = nextNoteId;
    }
}
