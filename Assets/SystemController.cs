using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SystemController : MonoBehaviour
{

    public GameObject Ship;
    public GameObject Asteroid;

    private GameObject ShipGO;
    private ShipController ShipController;
    private LinkController LinkController;
    private AsteroidManager AsteroidManager;

    private GameObject LinkedAsteroid;

    private bool IsLinked = false;

    private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "IsLinked":
                IsLinked = !IsLinked;
                if(!IsLinked)
                    ShipController.Unlink();
                else
                    ShipController.Link(LinkedAsteroid);
                break;
        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        ShipGO = Instantiate(Ship);
        AsteroidManager = new AsteroidManager();
        AsteroidManager.InstantiateAsteroids(Asteroid);
        ShipController = ShipGO.GetComponentInChildren<ShipController>();
        LinkController = ShipGO.GetComponentInChildren<LinkController>();
        LinkController.PropertyChanged += PropertyChangedHandler;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1")) // current_thrust > 0 &&
        {
            if(IsLinked)
            {
                LinkController.IsLinked = false;
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

                    LinkedAsteroid = AsteroidManager.GetClosestAsteroid(mousePosWorld);
                    LinkController.IsLinked = true;
                }
            }
        }

        if (ShipGO.transform.position.x > AsteroidManager.ChunkCenter.x + 250)
        {
            AsteroidManager.ReChunk(new Vector2(500, 0));
        }
    }
}
