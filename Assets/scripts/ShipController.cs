using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    float _screenMiddle = Screen.height / 2;
    public GameObject ship;
    public Image thrustUI;

    public float thrustForce = 30;
    private float torque_force = 300f;
    Rigidbody rb;

    private float direction = 0;

    private const float thrust_capacity = 100f;

    private float current_thrust = 100f;
    private float thrust_engaged = 0;
    public float thrust_delay = 0.5f;
    public float thrust_recovery = .05f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        thrustUI = GameObject.Find("BG").GetComponent<Image>();
    }
	
    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force);
    }


    public void ApplyTorque(Vector3 force)
    {
        float z_rot = rb.rotation.eulerAngles.z;
        if ((force.x < 0 && z_rot >= 30f) || (force.x > 0 && z_rot <= 55f))
        {
            rb.AddTorque(force);
        }
    }

    private void UpdateUI()
    {
        thrustUI.fillAmount = current_thrust / thrust_capacity;
    }

	// Update is called once per frame
	void Update ()
    {
        float y_Velocity; // = rb.velocity.y > 0 ? Mathf.Ceil(rb.velocity.y / rb.velocity.y) : Mathf.Floor( -1 * rb.velocity.y / rb.velocity.y);

        if (rb.velocity.y > 2.5f)
        {
            y_Velocity = Mathf.Ceil(rb.velocity.y / rb.velocity.y);
        }
        else if (rb.velocity.y < -2.5f)
        {
            y_Velocity = Mathf.Floor(-1 * rb.velocity.y / rb.velocity.y);
        }
        else
        {
            y_Velocity = 0;
        }

        if (y_Velocity != direction)
        {
            float z_rot = rb.rotation.eulerAngles.z;
            if(direction + y_Velocity == 0)
            {
                ApplyTorque(new Vector3(y_Velocity, 0, 0) * torque_force);
            }
            else
            {
                ApplyTorque(new Vector3(y_Velocity == 0 ? direction * -1 : y_Velocity, 0, 0) * torque_force / 2);
            }

            direction = y_Velocity;
        }

        //if(Input.GetButtonDown("Fire1"))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // - gameObject.transform.position;
        //    Plane plane = new Plane(Vector3.forward, transform.position);
        //    float dist = 0;
        //    if (plane.Raycast(ray, out dist))
        //    {
        //        Vector3 mousePos = ray.GetPoint(dist);
        //        mousePos.z = 0;
        //        mousePos.x = 0;
        //        mousePos.y -= ship.transform.position.y;
        //        mousePos = mousePos.normalized;
                
        //        Debug.Log(mousePos);
        //        ApplyTorque(mousePos * torque_force);
        //    }
        //}

        if (current_thrust > 0 && Input.GetButton("Fire1"))
        {
            current_thrust--;
            thrust_engaged = Time.time;

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

                mousePos = mousePos.normalized;
                //if(mousePos.y != direction)
                //{
                //    direction = mousePos.y;
                //    mousePos.x = mousePos.y;
                //    mousePos.y = 0;
                //    ApplyTorque(mousePos * torque_force);
                //}

                
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
        else if(thrust_engaged + thrust_delay <= Time.time)
        {
            current_thrust++;
            thrust_engaged += thrust_recovery;
        }

        UpdateUI();
    }
}
