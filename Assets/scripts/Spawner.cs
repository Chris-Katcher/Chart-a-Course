using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float frequency = 1f;
    public GameObject asteroid;
    public GameObject ship;

    private int current_asteroid = 0;
    private int asteroid_count = 20;
    private GameObject[] asteroids = new GameObject[200];

	// Use this for initialization
	void Start ()
    {
        for(int i = 0; i < asteroid_count; i++)
        {
            asteroids[i] = Instantiate(asteroid, new Vector3(25, 50, 0), Quaternion.identity);
        }

        for(int i = 0; i < asteroid_count; i++)
        {
            for(int j = i + 1; j < asteroid_count; j++)
            {
                Physics.IgnoreCollision(asteroids[i].GetComponent<Collider>(), asteroids[j].GetComponent<Collider>());
            }
        }
	}

    void UpdateFrequency()
    {
        if(frequency > 0.15f)
        {
            frequency *= 0.999f;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Time.time > nextActionTime)
        {
            nextActionTime += frequency;
            Ray ray = new Ray(ship.transform.position, ship.transform.TransformDirection(Quaternion.AngleAxis(20, ship.transform.up) * Vector3.forward) * 20);//Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1000);
            Vector3 pos = ray.GetPoint(20);
            float x_pos = pos.x;
            float y_pos = pos.y;
            //float y_pos = Random.value < .5 ? (Random.value - 0.5f) * 12 : ship.transform.position.y;
            asteroids[current_asteroid].transform.position = new Vector3(x_pos, y_pos, 0); //Random.value < .5 ? 1 : 
            current_asteroid++;
            if(current_asteroid == asteroid_count)
            {
                current_asteroid = 0;
            }
            UpdateFrequency();
        }


    }
}
