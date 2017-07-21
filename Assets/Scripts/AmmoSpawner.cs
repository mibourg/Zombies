using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour 
{
	public GameObject ammoPrefab;
	public GameObject ground;
	float maxTimer = 20f;
	float timer = 20f;

	void Start () 
	{
		
	}

	void Update () 
	{
		DecrementTimer();
		if(timer <= 0)
		{
			Instantiate(ammoPrefab, new Vector3(transform.position.x + RandomOffset(), transform.position.y), Quaternion.identity);
			timer = maxTimer;
		}
	}

	void DecrementTimer()
	{
		timer -= Time.deltaTime;
	}

	float RandomOffset()
	{
		return UnityEngine.Random.Range(-ground.transform.lossyScale.x / 2, ground.transform.lossyScale.x / 2);
	}
}
