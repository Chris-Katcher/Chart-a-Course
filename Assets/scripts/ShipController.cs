using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    float _screenMiddle = Screen.height / 2;
    public GameObject ship;

    public float thrustForce = 30;
    Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force);
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1"))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // - gameObject.transform.position;
            Plane plane = new Plane(Vector3.forward, transform.position);
            float dist = 0;
            if (plane.Raycast(ray, out dist))
            {
                Vector3 mousePos = ray.GetPoint(dist);
                mousePos.z = 0;
                mousePos.x = 0;
                mousePos.y -= ship.transform.position.y;
                Debug.Log(mousePos);
                ApplyForce(mousePos * thrustForce);
            }
            //mousePos.z = -10;
            //Camera.main.ScreenToWorldPoint(mousePos);

            //Vector3 mousePos = Input.mousePosition;
            //mousePos.y -= _screenMiddle;
            //mousePos = mousePos.normalized;
            //mousePos.x = 0;
            //mousePos.z = 0;
            //ApplyForce(mousePos * thrustForce);
        }
    }
}
