using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuHandler : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
    public void OnPlaySelected()
    {
        SceneManager.LoadScene("scenes/gameScene", LoadSceneMode.Single);
    }

	// Update is called once per frame
	void Update ()
    {
		
	}
}
