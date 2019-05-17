using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager
{
    private float nextActionTime = 0.0f;
    public float frequency = 1f;
    private GameObject asteroid;
    public GameObject ship;

    private int current_asteroid = 0;
    private readonly int asteroid_count;
    private List<GameObject> asteroids = new List<GameObject>();

    private static int DEFAULT_COUNT = 20;

	// Use this for initialization
	public AsteroidManager()
    {
        asteroid_count = DEFAULT_COUNT;
	}

    public AsteroidManager(int asteroidCount)
    {
        asteroid_count = asteroidCount;
    }

    public void InstantiateAsteroids(GameObject ast)
    {
        asteroid = ast;
        for (int i = 0; i < asteroid_count; i++)
        {
            asteroids.Add(GameObject.Instantiate(asteroid, new Vector3(10, 10, 0), Quaternion.identity));
        }

        for (int i = 0; i < asteroid_count; i++)
        {
            for (int j = i + 1; j < asteroid_count; j++)
            {
                Physics.IgnoreCollision(asteroids[i].GetComponent<Collider>(), asteroids[j].GetComponent<Collider>());
            }
        }
    }

    public GameObject GetClosestAsteroid(Vector3 mousePos)
    {
        GameObject closest = null;
        float astDist = float.MaxValue;
        foreach(GameObject ast in asteroids)
        {
            float dis = Vector3.Distance(mousePos, ast.transform.position);
            if(dis < astDist)
            {
                closest = ast;
                astDist = dis;
            }
        }
        return closest;
    }

    void UpdateFrequency()
    {
        if(frequency > 0.15f)
        {
            frequency *= 0.999f;
        }
    }
	
	//// Update is called once per frame
	//void Update ()
 //   {

 //       if (Time.time > nextActionTime)
 //       {
 //           nextActionTime += frequency;
 //           Ray ray = new Ray(ship.transform.position, ship.transform.TransformDirection(Quaternion.AngleAxis(20, ship.transform.up) * Vector3.forward) * 20);//Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1000);
 //           Vector3 pos = ray.GetPoint(20);
 //           float x_pos = pos.x;
 //           float y_pos = pos.y;
 //           //float y_pos = Random.value < .5 ? (Random.value - 0.5f) * 12 : ship.transform.position.y;
 //           asteroids[current_asteroid].transform.position = new Vector3(x_pos, y_pos, 0); //Random.value < .5 ? 1 : 
 //           current_asteroid++;
 //           if(current_asteroid == asteroid_count)
 //           {
 //               current_asteroid = 0;
 //           }
 //           UpdateFrequency();
 //       }


        

 //       foreach (GameObject ast in asteroids)
 //       {

 //       }
 //   }


}
