using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float gravity = 5f;

    public float speed = 1f;
    public float angle = 0f;

    private GameObject ship;
    private BoxCollider boxCol;

    void Awake()
    {
        ship = GameObject.Find("ShipContainer");
        boxCol = ship.GetComponent<BoxCollider>();
    }
    // Use this for initialization
    void Start ()
    {
        //gameObject.transform.Rotate(new Vector3(angle, 0, 0));
        float x_vel = Mathf.Cos(Mathf.Deg2Rad * angle) * speed;
        float y_vel = Mathf.Sin(Mathf.Deg2Rad * angle) * speed;
        
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x_vel, y_vel, 0));
	}

    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "ShipContainer")
        {
            Vector3 force = Vector3.Normalize(new Vector3(0, gameObject.transform.position.y - ship.transform.position.y, 0));
            force.y = force.y * gravity;
            ship.GetComponent<ShipController>().ApplyForce(force);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
