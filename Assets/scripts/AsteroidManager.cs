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
    private static float INTER_AST_DIST = 100;

    public Vector2 ChunkCenter { get; set; }

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
        ChunkCenter = new Vector2(0,0);
        asteroid = ast;
        for (int i = 0; i < asteroid_count; i++)
        {
            Vector3 pos = new Vector3((Random.value - .5f) * 500 + ChunkCenter.x, (Random.value - .5f) * 500 + ChunkCenter.y, 0);

            while(!CheckDistance(pos))
            {
                pos = new Vector3((Random.value - .5f) * 500 + ChunkCenter.x, (Random.value - .5f) * 500 + ChunkCenter.y, 0);
            }

            asteroids.Add(GameObject.Instantiate(asteroid, pos, Quaternion.identity));
        }

        for (int i = 0; i < asteroid_count; i++)
        {
            for (int j = i + 1; j < asteroid_count; j++)
            {
                Physics.IgnoreCollision(asteroids[i].GetComponent<Collider>(), asteroids[j].GetComponent<Collider>());
            }
        }
    }

    public void ReChunk(Vector2 center)
    {
        ChunkCenter = center;

        foreach(GameObject ast in asteroids)
        {
            ast.transform.Translate(new Vector3(0,0,INTER_AST_DIST * 2));
        }

        foreach (GameObject ast in asteroids)
        {
            Vector3 pos = new Vector3((Random.value - .5f) * 500 + ChunkCenter.x, (Random.value - .5f) * 500 + ChunkCenter.y, 0);

            while (!CheckDistance(pos))
            {
                pos = new Vector3((Random.value - .5f) * 500 + ChunkCenter.x, (Random.value - .5f) * 500 + ChunkCenter.y, 0);
            }
            ast.transform.position = pos;
        }
    }

    private bool CheckDistance(Vector3 newPos)
    {
        foreach(GameObject ast in asteroids)
        {
            if(Vector3.Distance(newPos, ast.transform.position) < INTER_AST_DIST)
            {
                return false;
            }
        }
        return true;
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
