using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* AUTHOR: Jason Patel
 * DESCRIPTION: This script handles enemy behavior (collision, word drawing, etc.) */

public class WordEnemy : MonoBehaviour {

    public string Word; // Enemy's word
    public float HitRange = 2f; // Tolerance for collision
    public float WordDrawOffsetX = -20;
    public float WordDrawOffsetY = -50;
    public bool Debugging = false;

    // Use this for initialization
    /*private void Start()
    {
		
	}*/
	
	// Update is called once per frame
	private void Update()
    {
        GameObject bullet = GameObject.FindGameObjectWithTag("Bullet"); // Check for bullet

        if (bullet != null) // If bullet is found
        {
            Vector3 bulletPos = bullet.transform.position;
            Vector3 pos = transform.position;

            // If bullet collides with enemy
            if (bulletPos.x >= (pos.x - HitRange) && bulletPos.x <= (pos.x + HitRange) &&
                bulletPos.y >= (pos.y - HitRange) && bulletPos.y <= (pos.y + HitRange))
            {
                // If word's first letter is the same as bullet's letter
                if (Word.ToCharArray()[0].Equals(bullet.GetComponent<LetterProjectile>().Letter))
                {
                    Destroy(gameObject); // Destroy this enemy
                    GameObject.Find("GM").GetComponent<GameManager>().Score++; // Add to score
                    
                }

                Destroy(bullet); // Destroy bullet regardless of letter match
                GameObject.Find("Turret").GetComponent<Turret>().ChangeLetter(); // Change letter or turret
                Destroy(GameObject.FindGameObjectWithTag("BulletContainer"));
            }
        }
	}

    private void OnGUI() // Draw enemy word on enemy texture
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        
        GUI.Label(new Rect(screenPos.x + WordDrawOffsetX, screenPos.y + WordDrawOffsetY, 200, 100), Word);
    }
}
