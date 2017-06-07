using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.
	private int random;


	void Start ()
	{
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		GameObject enemy = Instantiate(enemies[enemyIndex], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		random = (int)Random.Range(0.0f, 2.0f);
		if (random == 1) {
			Vector3 enemyScale = enemy.transform.localScale;
			enemyScale.x *= -1;
			enemy.transform.localScale = enemyScale;
		}
	}
}