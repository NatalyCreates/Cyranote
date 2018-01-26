using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ChoiceData {
    
    public int scoreEffect;
    public Enums.StoryFlavor flavor;

    public ChoiceData(int scoreEffect, Enums.StoryFlavor flavor) {
        this.scoreEffect = scoreEffect;
        this.flavor = flavor;
    }
}
