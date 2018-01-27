using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MGPlayerScript : MonoBehaviour
{

	[SerializeField] Rigidbody rb;
    [SerializeField] float speed;

	[SerializeField] MGLevelCreator LvCreator;
	[SerializeField] GameObject cam;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 velocity = rb.velocity;
        velocity.x = hor * speed * Time.deltaTime;
        velocity.z = ver * speed * Time.deltaTime;
        rb.velocity = velocity;
    }

	void OnTriggerEnter(Collider col)
	{
		transform.localPosition = Vector3.up;
		cam.transform.position = new Vector3(cam.transform.position.x ,transform.position.y, cam.transform.position.z);
		LvCreator.Reset ();
	}
}
