using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterScript : MonoBehaviour
{
    public Sprite neutral, positive, negative;

    public void SetMood(int score)
    {
        if (score < 0)
            GetComponent<SpriteRenderer>().sprite = negative;
        if (score == 0)
            GetComponent<SpriteRenderer>().sprite = neutral;
        if (score > 0)
            GetComponent<SpriteRenderer>().sprite = positive;
    }
}
