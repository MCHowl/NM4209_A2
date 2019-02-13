using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private LineRenderer line;
	private float flashFrequency = 5;

	private Vector2 velocity;
	private float velocity_rangeX = 0.75f;
	private float velocity_rangeY = 0.75f;

    void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		line = GetComponent<LineRenderer>();

		rb2d.velocity = GetNewVelocity();
		line.enabled = false;
    }

    void Update() {
		//print(Time.time % flashFrequency);

        if (Time.time % flashFrequency < flashFrequency / 2 && !line.enabled) {
			SetLinePosition();
			line.enabled = true;
		}

		if (Time.time % flashFrequency > flashFrequency / 2) {
			line.enabled = false;
		}
    }

	private void OnCollisionEnter2D(Collision2D collision) {
		rb2d.velocity = GetNewVelocity();
	}

	Vector2 GetNewVelocity() {
		return new Vector2(Random.Range(-velocity_rangeX, velocity_rangeX), Random.Range(-velocity_rangeY, velocity_rangeY));
	}

	void SetLinePosition() {
		line.SetPosition(0, transform.position + transform.up * 0.1f);
		line.SetPosition(1, transform.position - transform.up * 0.1f);
	}
}
