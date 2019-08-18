using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* AUTHOR: Jason Patel
 * DESCRIPTION: This script handles all turret behavior(firing, rotation, letter-changing, etc.) */

public class Turret : MonoBehaviour {

    private Transform _firePoint; // Starting point of all bullets
    private float _timeToSpawn; // Place in time for next shot
    
    public int RotationOffset = 0;
    public Transform BulletTrailPrefab; // Prefab for bullet container
    public Transform LetterPrefab; // Prefab for bullet letter
    public RaycastHit2D hit;
    public LayerMask toHit; // Limit what the turret can hit
    public float EffectSpawnRate = 10; // Rate of bullet creation
    public bool Debugging = false; 
    public int currLetterIndex; // Current letter sprite

    private void Awake()
    {
        _firePoint = transform.FindChild("FirePoint");
        if (_firePoint == null)
            Debug.Log("Error: FirePoint not found");
    }

    // Use this for initialization
    private void Start() {
        InvokeRepeating("ChangeLetter", .2f, 3);
    }
	
	// Update is called once per frame
	private void Update() {

        // Rotate turret on mouse move
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + RotationOffset);
        // ---------------------------

        // Limit amount of active bullets at a time
        bool ready = GameObject.FindGameObjectsWithTag("Bullet").Length == 0;

        // Shoot on left-click
        if (ready && Input.GetButtonDown("Fire1"))
            Shoot();

	}

    private void Shoot() // Fire bullet
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPos = new Vector2(_firePoint.position.x, _firePoint.position.y);
        hit = Physics2D.Raycast(firePointPos, mousePos - firePointPos, 100, toHit);

        if (Time.time >= _timeToSpawn)
        {
            Effect();
            _timeToSpawn = Time.time + 1 / EffectSpawnRate;
        }

        if (Debugging)
            Debug.DrawLine(firePointPos, mousePos, Color.blue);

        if (hit.collider != null)
        {
            if (Debugging)
                Debug.DrawLine(firePointPos, hit.point, Color.red);
        }
    }

    private void Effect() // Draw bullet trail effect
    {
        Instantiate(BulletTrailPrefab, _firePoint.position, _firePoint.rotation);
        Instantiate(LetterPrefab, _firePoint.position, _firePoint.rotation);
    }

    public void ChangeLetter() // Change turret's letter (next letter to fire)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0) // Check if any enemies are in play
        {
            // Get words of active enemies
            string[] currWords = new string[enemies.Length];

            int i = 0;
            foreach (GameObject enemy in enemies)
                currWords[i++] = enemy.GetComponent<WordEnemy>().Word;
            // ----------------------------

            if (Debugging)
            {
                string log = "";
                foreach (string s in currWords)
                    log += s.ToCharArray()[0];

                Debug.Log(log);
            }

            string selectWord = currWords[Random.Range(0, currWords.Length - 1)]; // Select one word of the active enemies' words
            char selectChar = selectWord.ToCharArray()[0]; // Get the first letter of that word

            // Set turret's letter to that letter
            int index;

            if (GameObject.Find("GM").GetComponent<GameManager>().LetterSpriteMap.TryGetValue(selectChar, out index))
                currLetterIndex = index;
            // ----------------------------------
        }
    }
}