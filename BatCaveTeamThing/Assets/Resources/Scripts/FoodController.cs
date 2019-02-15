using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
	private Rigidbody2D rb2d;
	private LineRenderer line;
	private float flashFrequency = 5;

	private Vector2 velocity;
	private float default_velocityX;
	private float default_velocityY;
	private float food_speed = 0.75f;

	private float fadeTime;
	private Color defaultColour;

	const string vertical = "vertical_wall";
	const string horizontal = "horizontal_wall";

    void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		line = GetComponent<LineRenderer>();

		default_velocityX = Random.Range(-1f, 1f);
		default_velocityY = Random.Range(-1f, 1f);
		Vector2 startingVelocity = new Vector2(default_velocityX, default_velocityY);
		rb2d.velocity = startingVelocity.normalized * food_speed;

		line.enabled = false;
		fadeTime = flashFrequency / 2;

		float r = Random.Range(0.5f, 1);
		float g = Random.Range(0.5f, 1);
		float b = Random.Range(0.5f, 1);
		defaultColour = new Color(r, g, b, 1);
    }

    void Update() {
		//print(Time.time % flashFrequency);

        if (Time.time % flashFrequency < flashFrequency / 2 && !line.enabled) {
			SetLinePosition();
			line.enabled = true;

			line.startColor = defaultColour;
			line.endColor = defaultColour;
		}

		if (Time.time % flashFrequency > flashFrequency / 2) {
			line.enabled = false;
		}

		if (line.enabled && line.startColor.a > 0) {
			decreaseAlpha();
		}
    }

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag(horizontal)) {
			default_velocityY *= -1;
			rb2d.velocity = new Vector2(default_velocityX, default_velocityY);
		} else if (collision.gameObject.CompareTag(vertical)) {
			default_velocityX *= -1;
			rb2d.velocity = new Vector2(default_velocityX, default_velocityY);
		}
	}

	void SetLinePosition() {
		line.SetPosition(0, transform.position + transform.up * 0.1f);
		line.SetPosition(1, transform.position - transform.up * 0.1f);
	}

	void decreaseAlpha() {
		float newAlpha = line.startColor.a;
		newAlpha = Mathf.Max(0, newAlpha - Time.deltaTime / fadeTime);
		Color newColor = new Color(defaultColour.r, defaultColour.g, defaultColour.b, newAlpha);

		line.startColor = newColor;
		line.endColor = newColor;
	}
}
