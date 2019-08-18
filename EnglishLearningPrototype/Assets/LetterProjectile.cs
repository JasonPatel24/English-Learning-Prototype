using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* AUTHOR: Jason Patel
 * DESCRIPTION: This script handles bullet movement and stores its letter */

public class LetterProjectile : MonoBehaviour {

    public char Letter; // Bullet letter
    public int MoveSpeed = 100; // Bullet speed
    public bool Debugging = false;

	// Use this for initialization
	/*private void Start() {
        
	}*/
	
	// Update is called once per frame
	private void Update() {
        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        Destroy(gameObject, 1.5f);
    }

}
