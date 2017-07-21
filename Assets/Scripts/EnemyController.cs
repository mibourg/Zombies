using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
	int health = 100;

	float speed = 4f;
	float jumpForce = 7f;

	GameObject player;

	Rigidbody rb;

	// Use this for initialization
	void Start() 
	{
		player = GameObject.Find("Player");
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		Move();
		CheckHealth();
	}

	void Move()
	{
		if (rb.constraints != RigidbodyConstraints.None)
		rb.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0.0f), speed * Time.deltaTime);
	}

	void Jump()
	{
		rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
	}

	void CheckHealth()
	{
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision hit)
	{
		BulletScript bulletComponent = hit.gameObject.GetComponent<BulletScript>();

		if (bulletComponent != null) {
			//health -= bulletComponent.damage;
			rb.constraints = RigidbodyConstraints.None;
			rb.AddExplosionForce(1000, hit.gameObject.transform.position, 3, 1.5f, ForceMode.Impulse);
			Destroy(hit.gameObject);
		}
	}
}
