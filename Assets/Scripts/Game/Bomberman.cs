using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomberman : MonoBehaviour {

    public float speed = 3.0f;
	public Rigidbody2D bomb;
	public int bombRadius = 2;
    public int bombCount  = 1;
    public string playerSuffix;

	// Private
	Animator animator;
	private Rigidbody2D rb;
	private SpriteRenderer sprite;
    private PauseMenu pm;

	void Awake () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
        pm = GameObject.Find("GameCanvas").GetComponent<PauseMenu>(); // Find the pause menu under the GameCanvas
	}

    private void Update() {
        /* Input */
        if (Input.GetButtonDown("DeployBomb" + playerSuffix) && !pm.IsGamePaused()) { // DeployBomb, if game not paused.
            Debug.Log("DeployBomb" + playerSuffix);
            if (bombCount > 0) {
                bombCount -= 1;
                animator.SetTrigger("setBomb");
                putBomb();
            }
        }
    }

    void FixedUpdate () {

        // Alternative movement code, using Unity Input Manager.
        /* Input */
        float moveHorizontal = Input.GetAxis("Horizontal" + playerSuffix);
        float moveVertical = Input.GetAxis("Vertical" + playerSuffix);

        /* Set BlendTree */
        if (Math.Abs(moveHorizontal) + Math.Abs(moveVertical) > 0) {
            animator.SetFloat("dirX", moveHorizontal);
            animator.SetFloat("dirY", moveVertical);
            animator.SetBool("walking", true);
        } else {
            animator.SetBool("walking", false);
        }

        /* Move Bomberman */
        rb.velocity = new Vector2(speed * moveHorizontal, speed * moveVertical);
        sprite.sortingOrder = 15 * 5 - ((int)(5 * rb.position[1]));  // Sprites with higher position.y values render on a lower order.
    }

	void putBomb() {
		Vector3 bombPosition = transform.position;
		bombPosition.x = (float) Math.Round (bombPosition.x);
		bombPosition.y = (float) (Math.Round (bombPosition.y + 0.4) - 1);
		Rigidbody2D bombInstance = Instantiate (bomb, bombPosition, Quaternion.identity);
		Bomb bombScript = bombInstance.GetComponent<Bomb> ();
		bombScript.parent = gameObject;
	}
}
