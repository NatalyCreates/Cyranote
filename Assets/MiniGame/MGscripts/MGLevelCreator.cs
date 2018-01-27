using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGLevelCreator : MonoBehaviour {

    public GameObject platformPre;
    public int numOfPlatforms;
    public float LevelWidth;
    public float minY;
    public float maxY;

	Vector3 PlatformPosition = new Vector3();

	List<GameObject> platforms = new List<GameObject>();

	// Use this for initialization
	void Start () {
		CreatePlatforms ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreatePlatforms()
	{
		for(int i = 0; i< numOfPlatforms; i++)
		{
			PlatformPosition.y += Random.Range (minY, maxY);
			PlatformPosition.x = Random.Range (-LevelWidth, LevelWidth);
			PlatformPosition.z = Random.Range (-LevelWidth, LevelWidth);

			platforms.Add(Instantiate(platformPre, PlatformPosition, Quaternion.Euler(0, Random.Range(0, 360) ,0), transform.parent));
			platforms [platforms.Count - 1].transform.localPosition = PlatformPosition;
		}
	}

	public void Reset()
	{
		for (int i = 0; i <  platforms.Count; i++) {
			Destroy (platforms[i]);
		}

		platforms.RemoveRange(0, platforms.Count);
		PlatformPosition = Vector3.zero;

		CreatePlatforms ();
	}
}
