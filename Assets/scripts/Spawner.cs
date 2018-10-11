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
    private GameObject[] asteroids = new GameObject[200];

	// Use this for initialization
	void Start ()
    {
        for(int i = 0; i < 200; i++)
        {
            asteroids[i] = Instantiate(asteroid, new Vector3(25, i*5, 0), Quaternion.identity);
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
            float y_pos = Random.value < .5 ? (Random.value - 0.5f) * 12 : ship.transform.position.y;
            asteroids[current_asteroid].transform.position = new Vector3(25, y_pos, Random.value < .5 ? 1 : 0);
            current_asteroid++;
            if(current_asteroid == 100)
            {
                current_asteroid = 0;
            }
            UpdateFrequency();
        }


    }
}
