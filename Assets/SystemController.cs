using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{

    public GameObject Ship;
    public GameObject Asteroid;

    private GameObject ShipGO;
    private ShipController ShipController;
    private AsteroidManager AsteroidManager;

    private bool IsLinked = false;
    // Start is called before the first frame update
    void Start()
    {
        ShipGO = Instantiate(Ship);
        AsteroidManager = new AsteroidManager();
        AsteroidManager.InstantiateAsteroids(Asteroid);
        ShipController = ShipGO.GetComponentInChildren<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) // current_thrust > 0 &&
        {
            if(IsLinked)
            {
                ShipController.Unlink();
                IsLinked = false;
            }

            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // - gameObject.transform.position;
                Plane plane = new Plane(Vector3.forward, ShipGO.transform.position);
                float dist = 0;
                if (plane.Raycast(ray, out dist))
                {
                    Vector3 mousePosWorld = ray.GetPoint(dist);

                    mousePosWorld.x -= gameObject.transform.position.x;
                    mousePosWorld.y -= gameObject.transform.position.y;
                    mousePosWorld.z -= gameObject.transform.position.z;
                    //Debug.Log(mousePos);
                    //ApplyForce(mousePos * thrustForce);
                    GameObject ast = AsteroidManager.GetClosestAsteroid(mousePosWorld);
                    ShipController.Link(ast);
                    IsLinked = true;
                    //mousePosWorld = mousePosWorld.normalized;
                }
            }

            
            //Debug.DrawRay(transform.position, transform.TransformDirection(Quaternion.AngleAxis(20, gameObject.transform.right) * Vector3.forward) * 1000, Color.yellow);
        }
    }
}
