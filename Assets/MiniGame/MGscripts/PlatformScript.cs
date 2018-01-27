using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
	[SerializeField] AudioClip[] sounds;
	
	void Update()
	{
		//if (transform.position.y + 10 < Camera.main.transform.position.y) {
		//	Destroy (gameObject);
		//}
	}

    void OnCollisionEnter(Collision collision)
    {
        float jumpFurce = 10f;

        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            Vector3 velocity = rb.velocity;
            velocity.y = jumpFurce;
            rb.velocity = velocity;

			AudioSource audioSource = GetComponent<AudioSource> ();
			audioSource.clip = sounds [Random.Range (0, sounds.Length)];
			audioSource.Play ();
        }
    }
}
