using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    float _screenMiddle = Screen.height / 2;
    public GameObject shipModel;
    public Image thrustUI;

    public float thrustForce = 30;
    private float torque_force = 300f;
    Rigidbody rb;
    
    private float direction = 0;

    private const float thrust_capacity = 500f;

    private float current_thrust = 500f;
    private float thrust_engaged = 0;
    public float thrust_delay = 0.5f;
    public float thrust_recovery = .05f;
    public float thrust_depletion = 0.75f;
    public float depletion_rate = 1f;

    public float max_velocity = 10f;

    public float rot_mult = 10f;

    private bool IsLinked = false;
    private GameObject LinkedObject = null;

    private Vector3 PreviousPosition = Vector3.zero;
    private Vector3 PreviousVelocity = Vector3.zero;

    private Vector3 Target = Vector3.zero;
    private float TargetDistance = 0.0f;

    private static readonly float LINK_THREASHOLD = 0;

    private float speed = 1f;

    private float Sign = 0f;

    // Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        ApplyForce(new Vector3(1000, 0, 0));
        //rb_model = shipModel.GetComponent<Rigidbody>();
        //thrustUI = GameObject.Find("BG").GetComponent<Image>();
    }
	
    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force);
        //if (rb.velocity.magnitude < max_velocity)
        //{
            
        //}
        
    }

    private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "IsLinked":
                IsLinked = !IsLinked;
                break;
        }
    }
    public void ApplyTorque(Vector3 force)
    {
        float z_rot = rb.rotation.eulerAngles.z;
        if ((force.x < 0 && z_rot >= 30f) || (force.x > 0 && z_rot <= 55f))
        {
            //rb_model.AddTorque(force);
        }
    }

    public void ApplyRotation(Vector3 rot)
    {
        rb.transform.rotation = Quaternion.Euler(rot);
    }

    public void Link(GameObject ast)
    {
        IsLinked = true;
        LinkedObject = ast;
        PreviousPosition = transform.position;
        TargetDistance = Vector3.Distance(transform.position, LinkedObject.transform.position);
        Target = (ast.transform.position - transform.position);

        Sign = -Mathf.Sign(Target.x * Target.y);
        speed = 1f + Time.deltaTime * .1f;
        
    }

    public void Unlink()
    {
        IsLinked = false;
        LinkedObject = null;
        Debug.Log(PreviousVelocity);
        speed = 1f - Time.deltaTime * .1f;
    }

    public void CalculateRotation()
    {
        Vector3 vel = rb.velocity;
        float rot = vel.y * rot_mult;
      
        ApplyRotation(new Vector3(0, 90, 45 + rot));
        //rb.AddTorque(new Vector3(0, vel.y, 0));
        //Debug.Log();
    }

    private void UpdateUI()
    {
        thrustUI.fillAmount = current_thrust / thrust_capacity;
    }

    private void ApplyLinkForce()
    {

        //if (Vector3.Distance(transform.position, LinkedObject.transform.position) >= TargetDistance + LINK_THREASHOLD)
        // {
        Vector3 linkDirectional = (LinkedObject.transform.position - transform.position).normalized;
       if (Mathf.Sign(linkDirectional.x) != Mathf.Sign(rb.velocity.x) &&
            Mathf.Sign(linkDirectional.y) != Mathf.Sign(rb.velocity.y))
        {
            ApplyForce((LinkedObject.transform.position - transform.position).normalized * 75f);
        }
       else
        {
            ApplyForce((LinkedObject.transform.position - transform.position).normalized * 7.5f);
        }
            ApplyForce((LinkedObject.transform.position - transform.position).normalized * 7.5f);
        //transform.right = Vector3.RotateTowards(transform.right, Target, 2f * Time.deltaTime, 0.0f); // working rotation
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, rb.velocity.normalized);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);


        //rb.velocity = transform.right * rb.velocity.magnitude * speed ;

        //}
    }

	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.right * 1000, Color.yellow);
        
        if (IsLinked)
        {
            Target =  (LinkedObject.transform.position -  transform.position); //Quaternion.Euler(0, 0, Sign*90) *
            Debug.DrawRay(transform.position, Target.normalized *1000, Color.blue);
            ApplyLinkForce();
        }
        else
        {
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, rb.velocity.normalized);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 3);
            //transform.right = Vector3.RotateTowards(transform.right, rb.velocity, Time.deltaTime, 0.0f);
            rb.velocity = transform.right * rb.velocity.magnitude * speed;
        }
    }
}
