using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* AUTHOR: Jason Patel
 * DESCRIPTION: This script modifies the letter cursor sprite */

public class DisplayLetter : MonoBehaviour {

    public Object[] Letters; // Store all letter sprites

    public bool Debugging = false;

	// Use this for initialization
	private void Start()
    {
        Letters = Resources.LoadAll("Letters"); // Get letter sprites

        if (Debugging)
        {
            foreach (Object o in Letters)
            {
                Debug.Log("Name is: " + o.name + ", Type is: " + o.GetType());
            }
        }
	}
	
	// Update is called once per frame
	private void Update()
    {
        int index = GameObject.Find("Turret").GetComponent<Turret>().currLetterIndex + 1; // Add 1 to exclude the letters texture (we only want the sprites)
        GetComponent<SpriteRenderer>().sprite = (Sprite)Letters[index]; // Set sprite
	}
}
