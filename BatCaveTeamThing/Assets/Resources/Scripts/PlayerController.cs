using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private float horizontal, vertical;

	public Transform sprite_mask;

	public float speed;
	private float flashFrequency = 5;

	private int startingHealth = 10;
	private bool canReduceHealth = true;

	public int score { get; set;}
	public int health { get; set; }

	private const string FOOD_TAG = "Finish";

    void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		score = 0;
		health = startingHealth;
    }

    // Update is called once per frame
    void Update() {
		if (Time.time % flashFrequency < flashFrequency / 2 && canReduceHealth) {
			health--;
			canReduceHealth = false;
			print("Health: " + health);
		}

		if (Time.time % flashFrequency > flashFrequency / 2)
		{
			canReduceHealth = true;
		}

		if (health <= startingHealth) {
			sprite_mask.localPosition = new Vector3(0, ((float)health/(float)startingHealth) - 1, 0);
		} else {
			sprite_mask.localPosition = Vector3.zero;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag(FOOD_TAG)) {
			Destroy(collision.gameObject);
			score++;
			health++;
		}
	}

	void FixedUpdate() {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		rb2d.velocity = new Vector2(horizontal, vertical) * speed;
	}
}
