using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Explosion : MonoBehaviour {

    public float duration = 1f;
    public LayerMask playerLayer;
    public LevelLoader levelLoader;

    private WaitForSeconds endTimer;
    private Vector3 size;

	// Use this for initialization
	void Start () {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        endTimer = new WaitForSeconds(3f);
        Destroy(gameObject, duration);
    }

    //Set variables. Need to take blastRadius from the bomb that spawned this
    private void Awake()
    {
        //radius = GetComponentInParent<BombScript>().blastRadius;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        GameObject target = other.gameObject;
        Debug.Log(target);
        Debug.Log(target.layer);
        if (target.tag == "player")
        {
            Debug.Log("Collision with player at: " + transform.position);
            Destroy(target);
            Debug.Log("ending...");
            //StartCoroutine(gameEnd());
            levelLoader.LoadLevel("StartMenu");
        } else if (target.tag == "brick") 
        {
            Debug.Log("Transform of explosion: " + transform.position);
            Tilemap bricks = other.gameObject.GetComponent<Tilemap>();
            Debug.Log("Tilemap at: " + bricks.WorldToCell(transform.position));
            Debug.Log("Tilemap is: " + bricks.GetType());
            Tile brick = bricks.GetTile<Tile>(bricks.WorldToCell(transform.position));

            //if (brick != null) {
            //    Debug.Log("brick is: " + brick.GetType());
            //}
        }
    }

    IEnumerator gameEnd()
    {
        yield return endTimer;
    }
}
