using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    public static AudioManager Instance;
	public AudioSource backgroundMusicSource;  
    public AudioSource soundSource;  
	[SerializeField] AudioClip[] badReactionSounds;
    [SerializeField] AudioClip[] goodReactionSounds;
    [SerializeField] AudioClip[] normalReactionSounds;
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
            case Enums.SoundType.Good:
                randClip = Random.Range(0, goodReactionSounds.Length);
                soundSource.clip = goodReactionSounds[randClip];
                break;
            case Enums.SoundType.Bad:
                randClip = Random.Range(0, badReactionSounds.Length);
                soundSource.clip = badReactionSounds[randClip];
                break;
            case Enums.SoundType.Normal:
                randClip = Random.Range(0, normalReactionSounds.Length);
                soundSource.clip = normalReactionSounds[randClip];
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