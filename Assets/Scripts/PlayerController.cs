using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	int health = 100;

	float knockbackForce = 10f;
	bool invincible = false;
	float knockbackTimer = 0f;
	float maxKnockbackTimer = 1f;

	float speed = 7f;
	float jumpForce = 5f;

	float bulletForce = 2500f;
	int ammo = 20;

	public GameObject bulletPrefab;
	List<GameObject> bullets = new List<GameObject>();
	float maxBulletDeleteTimer = 1f;
	float bulletDeleteTimer;

	public Text ammoText;
	public Text healthText;

	Rigidbody rb;

	void Start () 
	{
		rb = GetComponent<Rigidbody>();	
	}

	void Update () 
	{
		if(invincible == false)
		{
			Move();
		}

		DecrementBulletTimer();

		if(bulletDeleteTimer <= 0f && bullets.Count >= 1)
		{
			RemoveBullet();
			bulletDeleteTimer = maxBulletDeleteTimer;
		}

		ammoText.text = "Ammo: " + ammo.ToString();
		healthText.text = "Health: " + health.ToString();

		if(invincible == true)
		{
			DecrementKnockbackTimer();
			if(knockbackTimer <= 0f)
			{
				invincible = false;
			}
		}

		if(health <= 0)
		{
			Destroy(gameObject);
		}

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(ammo > 0)
			{
				Shoot();
			}
		}

		if(Physics.Raycast(transform.position, Vector3.down, 1.1f))
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
		}

		if(Input.GetKeyDown(KeyCode.A))
		{
			transform.rotation = Quaternion.Euler(0.0f, 270f, 0.0f);
		}

		else if(Input.GetKeyDown(KeyCode.D))
		{
			transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
		}
	}

	void Move()
	{
		transform.position += new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f) * speed * Time.deltaTime;
	}

	void Jump()
	{
		rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
	}

	void Shoot()
	{
		if(transform.rotation == Quaternion.Euler(0.0f, 270f, 0.0f))
		{
			GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x - 1.1f, transform.position.y), Quaternion.identity) as GameObject;
			bullets.Add(bullet);
			bulletDeleteTimer = maxBulletDeleteTimer;
			Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
			bulletRb.AddForce(transform.forward * bulletForce, ForceMode.Force);
			ammo -= 1;
		}
		else
		{
			GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x + 1.1f, transform.position.y), Quaternion.identity) as GameObject;
			bullets.Add(bullet);
			bulletDeleteTimer = maxBulletDeleteTimer;
			Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
			bulletRb.AddForce(transform.forward * bulletForce, ForceMode.Force);

			ammo -= 1;
		}
	}

	void DecrementKnockbackTimer()
	{
		knockbackTimer -= Time.deltaTime;
	}

	void DecrementBulletTimer()
	{
		bulletDeleteTimer -= Time.deltaTime;
	}

	void RemoveBullet()
	{
		Destroy(bullets[0]);
		bullets.RemoveAt(0);
	}

	void OnCollisionEnter(Collision hit)
	{
		if(hit.gameObject.CompareTag("Ammo"))
		{
			ammo += 20;
			if(ammo > 20)
			{
				ammo = 20;
			}
			Destroy(hit.gameObject);
		}

		if(hit.gameObject.CompareTag("Enemy"))
		{
			if(invincible == false)
			{
				health -= 20;
				if(health < 0)
				{
					health = 0;
				}

				invincible = true;
				knockbackTimer = maxKnockbackTimer;
				rb.AddForce(hit.gameObject.transform.forward * knockbackForce, ForceMode.VelocityChange);
			}
		}
	}
}
