using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataLoader {

    static readonly string storyPath = "Story/story";

    public static StoryData LoadStory() {
        TextAsset t = Resources.Load<TextAsset>(storyPath);
        return JsonUtility.FromJson<StoryData>(t.text);
    }
}
