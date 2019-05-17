using System.Collections;
using System.Collections.Generic;
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

    private static readonly float LINK_THREASHOLD = 1.10f;

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
        //rb.velocity = Vector3.zero;
        TargetDistance = Vector3.Distance(transform.position, LinkedObject.transform.position);
        Target = Quaternion.Euler(0, 0, -90) * (ast.transform.position - transform.position); 

        //transform.right = target;


        //Vector3 vecToAst = ast.transform.position - transform.position;
        //Vector3 dir = Vector3.Cross(vecToAst, Vector3.forward);
        //float mag = vecToAst.magnitude;
        //transform.LookAt(dir.normalized);
    }

    public void Unlink()
    {
        IsLinked = false;
        LinkedObject = null;
        //rb.velocity = PreviousVelocity;
        Debug.Log(PreviousVelocity);
    }

    public void CalculateRotation()
    {
        Vector3 vel = rb.velocity;
        float rot = vel.y * rot_mult;
        //Vector3 dir = gameObject.transform.TransformDirection(Vector3.forward).normalized;
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.yellow);
        //Vector3 z_rot = vel.magnitude * gameObject.transform.TransformDirection(Vector3.forward).normalized;
        //Debug.DrawRay(transform.position, z_rot * 1000, Color.yellow);
        //Debug.Log(z_rot);
        //float dot_product = Vector3.Dot(vel, dir);
        //float angle_sign = Mathf.Sign(vel.y) * Mathf.Sign(dir.y) * Mathf.Sign(dir.y);

        //Debug.Log(dir);
        //if (vel.magnitude * dir.magnitude > 0)
        //{
        //    float rot = rot_mult * angle_sign * Mathf.Rad2Deg * (Mathf.Acos(dot_product / (vel.magnitude * dir.magnitude)));

        //}
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

        if (Vector3.Distance(transform.position, LinkedObject.transform.position) >= TargetDistance + LINK_THREASHOLD)
        {
            transform.right = Vector3.RotateTowards(transform.right, Target, Time.deltaTime, 0.0f);

            //float angle = Vector3.Angle(Target, transform.right);

            //transform.right = Quaternion.AngleAxis( angle, Vector3.forward) * transform.right;
            //transform.RotateAround(LinkedObject.transform.position, Vector3.forward, 30 * Time.deltaTime);
            //Vector3.RotateTowards(rb.velocity, Target, Time.deltaTime, 0.0f);
            rb.velocity = transform.right * rb.velocity.magnitude;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(transform.position, transform.right * 1000, Color.yellow);
        
        if (IsLinked)
        {
            Target = Quaternion.Euler(0, 0, -90) * (LinkedObject.transform.position -  transform.position);
            Debug.DrawRay(transform.position, Target.normalized *1000, Color.blue);
            ApplyLinkForce();
            //transform.RotateAround(LinkedObject.transform.position, Vector3.forward, 20 * Time.deltaTime); //rb.velocity.magnitude
            //PreviousVelocity = (transform.position - PreviousPosition) / Time.deltaTime;
            //PreviousPosition = transform.position;
            //transform.right = Vector3.RotateTowards(transform.right, Target, Time.deltaTime, 0.0f);
        }
        
        //    float y_Velocity; // = rb.velocity.y > 0 ? Mathf.Ceil(rb.velocity.y / rb.velocity.y) : Mathf.Floor( -1 * rb.velocity.y / rb.velocity.y);

        //    if (rb.velocity.y > 2.5f)
        //    {
        //        y_Velocity = Mathf.Ceil(rb.velocity.y / rb.velocity.y);
        //    }
        //    else if (rb.velocity.y < -2.5f)
        //    {
        //        y_Velocity = Mathf.Floor(-1 * rb.velocity.y / rb.velocity.y);
        //    }
        //    else
        //    {
        //        y_Velocity = 0;
        //    }

        //    if (y_Velocity != direction)
        //    {
        //        if(direction + y_Velocity == 0)
        //        {
        //            ApplyTorque(new Vector3(y_Velocity, 0, 0) * torque_force);
        //        }
        //        else
        //        {
        //            ApplyTorque(new Vector3(y_Velocity == 0 ? direction * -1 : y_Velocity, 0, 0) * torque_force / 2);
        //        }

        //        direction = y_Velocity;
        //    }


        //    if ( Input.GetButton("Fire1")) // current_thrust > 0 &&
        //    {
        //        current_thrust-= depletion_rate;
        //        thrust_engaged = Time.time;

        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // - gameObject.transform.position;
        //        Plane plane = new Plane(Vector3.forward, transform.position);
        //        float dist = 0;
        //        if (plane.Raycast(ray, out dist))
        //        {
        //            Vector3 mousePos = ray.GetPoint(dist);
        //            mousePos.z = 0;
        //            mousePos.x = 0;
        //            mousePos.y -= gameObject.transform.position.y;
        //            //Debug.Log(mousePos);
        //            ApplyForce(mousePos * thrustForce);

        //            mousePos = mousePos.normalized;
        //        }

        //    }
        //    else if((thrust_engaged + thrust_delay <= Time.time) && current_thrust < thrust_capacity)
        //    {
        //        current_thrust++;
        //        thrust_engaged += thrust_recovery;
        //    }
        //    //Debug.DrawRay(transform.position, transform.TransformDirection(Quaternion.AngleAxis(20, gameObject.transform.right) * Vector3.forward) * 1000, Color.yellow);
        //    if(rb.velocity.magnitude < 10)
        //    {
        //        //ApplyForce(transform.right * 10 );
        //    }
        //    //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
        //    CalculateRotation();
        //    UpdateUI();
    }
}
