using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BombermanMovement : MonoBehaviour {

	public string down  = "s";
	public string right = "d";
	public string up  = "w";
	public string left    = "a";
	public float speed = 2f;

	Animator animator;
	private Rigidbody2D rb;

	private int direction = 0;


	void Awake () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {

		/* Movement */
		int moveX = 0;
		int moveY = 0;

		int facing = -1;

		/* Get Input */
		if (Input.GetKey (down)) {
			moveY -= 1;
			if (facing == -1) {
				facing = 0;
			}
		}
			
		if (Input.GetKey (right)) {
			moveX += 1;
			if (facing == -1) {
				facing = 1;
			}
		}
				
		if (Input.GetKey (up)) {
			moveY += 1;
			if (facing == -1) {
				facing = 2;
			}
		}
					
		if (Input.GetKey (left)) {
			moveX -= 1;
			if (facing == -1) {
				facing = 3;
			}
		}

		if (facing != -1) {
			animator.SetInteger ("Facing", facing);
		}

		animator.SetInteger ("IsMoving", Math.Abs (moveX) + Math.Abs (moveY));

		Vector2 movement = new Vector2 (moveX * speed, moveY * speed);
		rb.position += movement * Time.deltaTime;
	}
}
