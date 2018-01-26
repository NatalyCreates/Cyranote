using System.Collections.Generic;

[System.Serializable]
public class OptionData {

    public string text;
    public int scoreEffect;
    public Enums.StoryFlavor flavor;

    public OptionData(string text, int scoreEffect, Enums.StoryFlavor flavor) {
        this.text = text;
        this.scoreEffect = scoreEffect;
        this.flavor = flavor;
    }
}
