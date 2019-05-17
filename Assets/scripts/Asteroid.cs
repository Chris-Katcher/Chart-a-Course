using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float gravity = 20f;
    public float mass = 10f;
    public float speed = 1000f;
    public float angle = 0f;

    
    private GameObject ship;
    private BoxCollider boxCol;

    void Awake()
    {
        //ship = GameObject.Find("ShipModel");
       // boxCol = ship.GetComponent<BoxCollider>();
    }
    // Use this for initialization
    void Start ()
    {
        //gameObject.transform.Rotate(new Vector3(angle, 0, 0));
        float x_vel = Mathf.Cos(Mathf.Deg2Rad * angle) * speed;
        float y_vel = Mathf.Sin(Mathf.Deg2Rad * angle) * speed;
        
        //gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x_vel, y_vel, 0));
	}

    private void OnTriggerStay(Collider other)
    {
       
        //if (other.gameObject.tag == "Ship")
        //{
        //    Vector3 dist = new Vector3(gameObject.transform.position.x - ship.transform.position.x, gameObject.transform.position.y - ship.transform.position.y, 0);
        //    float grav_force = gravity * mass / (Mathf.Pow(dist.magnitude, 2));
        //    Vector3 force = Vector3.Normalize(dist);
        //    force.y = force.y * Mathf.Min(grav_force, 30);
        //    force.x = force.x * Mathf.Min(grav_force, 30);
        //    ship.GetComponent<ShipController>().ApplyForce(force);
        //}
    }

    // Update is called once per frame
    void Update ()
    {
       
    }

    public void UpdateDistance(float dist, bool isClosest)
    {

    }
}
