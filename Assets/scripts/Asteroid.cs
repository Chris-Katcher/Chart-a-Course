using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float speed = 1f;
    public float angle = 0f;
    
    // Use this for initialization
    void Start ()
    {
        //gameObject.transform.Rotate(new Vector3(angle, 0, 0));
        float x_vel = Mathf.Cos(Mathf.Deg2Rad * angle) * speed;
        float y_vel = Mathf.Sin(Mathf.Deg2Rad * angle) * speed;
        
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x_vel, y_vel, 0));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
