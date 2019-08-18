using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* AUTHOR: Jason Patel
 * DESCRIPTION: This script handles all elements of the player HUD (Score, timer, etc.) and properly displays the game over screen when game ends */

public class GameManager : MonoBehaviour {

    public int Score = 0; // Keep track of player score
    public Dictionary<char, int> LetterSpriteMap; // Map chars to their matching letter sprites
    public Dictionary<int, char> SpriteLetterMap; // Map letter sprites to their matching chars
    public float RemainingTime = 120f; // How long the game lasts (in seconds)
    public bool Debugging = false;

    private string _alphabet = "abcdefghijklmnopqrstuvwxyz";
    private bool _isOver = false; // Is the game over?

    private void Awake()
    {
        LetterSpriteMap = new Dictionary<char, int>();
        SpriteLetterMap = new Dictionary<int, char>();

        // Populate dictionaries
        int i = 0;
        foreach (char c in _alphabet.ToCharArray())
        {
            if (i == 18)
                LetterSpriteMap.Add(c, ++i);

            else
                LetterSpriteMap.Add(c, i);

            SpriteLetterMap.Add(i, c);
            i++;
        }
        // ----------------------
    }

    // Use this for initialization
    /*private void Start()
    {
        
	}*/
	
	// Update is called once per frame
	private void Update()
    {
        if (RemainingTime > 0)
            RemainingTime -= Time.deltaTime; // Decrease time

        else
            gameOver(); // If time's up, end game
	}

    private void OnGUI()
    {
        int minutes = Mathf.FloorToInt(RemainingTime / 60f);
        int seconds = Mathf.RoundToInt(RemainingTime % 60f);

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        if (_isOver) // When game ends, display game over text and final score
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(Vector3.zero);
            GUI.Label(new Rect(screenPos.x - 100, screenPos.y, 500, 100), "Game Over! You scored " + Score + " points!");
        }

        else
        {
            GUI.Label(new Rect(10, 10, 200, 100), "Score: " + Score); // Display points accumulated
            GUI.Label(new Rect(10, 25, 200, 100), "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00")); // Display remaining time
        }
    }

    // End the game
    private void gameOver()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);

        Destroy(GameObject.FindGameObjectWithTag("Bullet"));
        Destroy(GameObject.FindGameObjectWithTag("BulletContainer"));
        Destroy(GameObject.Find("Turret"));
        Destroy(GameObject.Find("EnemySpawner"));
        Destroy(GameObject.Find("LetterCursor"));

        _isOver = true;
    }
}
