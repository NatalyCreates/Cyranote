using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager Instance;
	public AudioSource backgroundMusicSource;  
    public AudioSource soundSource;
	[SerializeField] AudioClip[] goodReactionASounds;
    [SerializeField] AudioClip[] goodReactionBSounds;
    [SerializeField] AudioClip[] badReactionASounds;
    [SerializeField] AudioClip[] badReactionBSounds;
    [SerializeField] AudioClip[] normalReactionASounds;
    [SerializeField] AudioClip[] normalReactionBSounds;
    [SerializeField] AudioClip[] PaperSounds;
    [SerializeField] AudioClip[] PencilSounds;

    public void Awake() {
        if (Instance == null) Instance = this;
        else throw new System.Exception("AudioManager class is Singleton, but has more than 1 instance!");
    }

    public void Start() {

    }

    void Update() {
        
    }

	public void PlaySound(Enums.SoundType soundType)
	{
        int randClip = 0;
        switch (soundType)
        {
            case Enums.SoundType.GoodA:
                randClip = Random.Range(0, goodReactionASounds.Length);
                soundSource.clip = goodReactionASounds[randClip];
                break;
            case Enums.SoundType.GoodB:
                randClip = Random.Range(0, goodReactionBSounds.Length);
                soundSource.clip = goodReactionBSounds[randClip];
                break;
            case Enums.SoundType.BadA:
                randClip = Random.Range(0, badReactionASounds.Length);
                soundSource.clip = badReactionASounds[randClip];
                break;
            case Enums.SoundType.BadB:
                randClip = Random.Range(0, badReactionBSounds.Length);
                soundSource.clip = badReactionBSounds[randClip];
                break;
            case Enums.SoundType.NormalA:
                randClip = Random.Range(0, normalReactionASounds.Length);
                soundSource.clip = normalReactionASounds[randClip];
                break;
            case Enums.SoundType.NormalB:
                randClip = Random.Range(0, normalReactionBSounds.Length);
                soundSource.clip = normalReactionBSounds[randClip];
                break;
            case Enums.SoundType.Paper:
                randClip = Random.Range(0, PaperSounds.Length);
                soundSource.clip = PaperSounds[randClip];
                break;
            case Enums.SoundType.Pencil:
                randClip = Random.Range(0, PencilSounds.Length);
                soundSource.clip = PencilSounds[randClip];
                break;
        }
        soundSource.Play();
	}

}