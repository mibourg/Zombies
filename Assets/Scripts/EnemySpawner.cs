using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject enemyPrefab; 
	public GameObject ground;

	float timer = 5f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		DecrementTimer();

		if(timer <= 0)
		{
			SpawnEnemy();
			timer = RandomTimer();
		}
	}

	void DecrementTimer()
	{
		timer -= Time.deltaTime;
	}

	void SpawnEnemy()
	{
		Vector3 side = RandomSide();
		if(side == new Vector3(ground.transform.lossyScale.x / 2, 0f))
		{
			Instantiate(enemyPrefab, transform.position + side, Quaternion.Euler(0.0f, -90.0f, 0.0f));
		}
		else
		{
			Instantiate(enemyPrefab, transform.position + side, Quaternion.Euler(0.0f, 90.0f, 0.0f));
		}
	}

	float RandomTimer()
	{
		return Random.Range(3f, 10f);
	}

	Vector3 RandomSide()
	{
		int test = Random.Range(1, 3);
		if(test == 1)
		{
			return new Vector3(ground.transform.lossyScale.x / 2, 0f);
		}
		else
		{
			return new Vector3(-ground.transform.lossyScale.x / 2, 0f);
		}
	}
}
