using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* AUTHOR: Jason Patel
 * DESCRIPTION: This script uses the letter cursor's sprite to set its bullet's letter */

public class SetSprite : MonoBehaviour {

    public bool Debugging = false;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = GameObject.Find("LetterCursor").GetComponent<SpriteRenderer>().sprite;

        char letter; // Bullet's letter
        string spriteName = GetComponent<SpriteRenderer>().sprite.name; // Get sprite renderer
        string spriteNum = spriteName.Split('_')[1]; // Get sprite number
        Dictionary<int, char> letterLookup = GameObject.Find("GM").GetComponent<GameManager>().SpriteLetterMap; // Map sprites to letters

        // Match sprite number to corresponding letter and set bullet's letter to it
        if (letterLookup.TryGetValue(int.Parse(spriteNum), out letter))
            GetComponent<LetterProjectile>().Letter = letter;
    }

    // Use this for initialization
    private void Start()
    {   
        if (Debugging)
            Debug.Log(GetComponent<LetterProjectile>().Letter);
	}
	
	// Update is called once per frame
	/*private void Update()
    {
		
	}*/
}
