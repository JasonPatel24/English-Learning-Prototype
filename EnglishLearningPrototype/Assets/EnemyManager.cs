using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* AUTHOR: Jason Patel
 * DESCRIPTION: This script handles spawning of enemies and assigns them a random word from the words text file */

public class EnemyManager : MonoBehaviour {

    const int NUM_OF_LINES = 995; // Number of lines in words text file

    public int SpawnLimit = 3; // Max number of enemies active at a time
    public float SpawnRate = .5f; // Speed of enemy spawn
    public GameObject EnemyPrefab; // Enemy object to spawn
    public bool Debugging = false;

    // Use this for initialization
    private void Start()
    {
        // Start with multiple enemies, then continue spawning at a fixed rate
        for (int i = 0; i < SpawnRate; i++)
            Spawn();

        InvokeRepeating("Spawn", .2f, SpawnRate);
	}
	
	// Update is called once per frame
	/*private void Update()
    {
		
	}*/

    private void Spawn() // Spawn Enemy
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < SpawnLimit) // If we can still spawn enemies
        {
            // Get x positions of active enemies
            List<float> enemyPositions = new List<float>();
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                enemyPositions.Add(enemy.transform.position.x);

            // Determine x of new enemy
            float xPos;

            do
            {
                xPos = Random.Range(-20, 20); // Get new x
            } while (enemyPositions.Contains(xPos)); // while current x is occupied by an enemy

            // Create new enemy
            GameObject newEnemy = Instantiate(EnemyPrefab, new Vector3(Random.Range(-20, 20), EnemyPrefab.transform.position.y), 
                EnemyPrefab.transform.rotation);

            newEnemy.name = "Enemy_" + GameObject.FindGameObjectsWithTag("Enemy").Length; // Uniquely identify new enemy

            StreamReader wordReader = new StreamReader("Assets/EnglishLearningWords.txt"); // Reader for words text file
            int lineNum = Random.Range(1, NUM_OF_LINES); // Get random line number for text file

            // Return word at line number
            string line = "";
            for (int i = 0; i < lineNum; i++) // Until we've reached line number
            {
                line = wordReader.ReadLine();
            }

            newEnemy.GetComponent<WordEnemy>().Word = line; // Enemy word is last line returned

            if (Debugging)
                Debug.Log(newEnemy.GetComponent<WordEnemy>().Word);
        }
    }
}
