using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public GameObject parent;
	public int bombRadius = 2;
    public float timer = 3f;
    private Vector3 size;
    public GameObject explosion;

	Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
		StartCoroutine (Explode ());
	}

	void Update() {
	}

	IEnumerator Explode() {
		yield return new WaitForSeconds (timer);
		Bomberman bomberMan = parent.GetComponent<Bomberman> ();
        explode();
		bomberMan.bombCount += 1;
		Destroy (gameObject);
	}

    public void explode()
    {
        GameObject boom = Instantiate(explosion, transform.position, transform.rotation);
        size = boom.GetComponent<Collider2D>().bounds.size;
        makeExplosion();
    }

    bool[] checkAdjacentTiles(Transform pos)
    //TODO: Use transform to find current tile, then return if adjacent tiles are available.
    {
        bool[] adjacents = new bool[4];
        bool[] adjacentsTest = { true, true, true, true };
        return adjacentsTest;
    }

    //Spawns explosions blocks in all available directions.
    void makeExplosion()
    {
        //Debug.Log("here explosion: " + transform.position);
        bool[] adjacent = checkAdjacentTiles(transform);

        for (int i = 0; i < 4; i++)
        {
            Transform place = transform;
            //If the direction is free, spawn explosions with increasing offsets in that direction. 
            if (adjacent[i])
            {
                //Debug.Log("here adjacent");
                for (int j = 0; j < bombRadius; j++)
                {
                    //Debug.Log("here:" + place.transform.position);
                    GameObject newExplosion = Instantiate(explosion, place.position + (findOffset(i) * (j + 1)), transform.rotation);

                    //Do not continue growing the explosion if the direction is no longer free. Check using the transform of the new explosion
                    //Check only the growth direction.
                    if (!checkAdjacentTiles(newExplosion.transform)[i])
                    {
                        break;
                    }
                }
            }
        }
    }

    //Finds an offset to add to transform in order to spawn the next explosion block.
    Vector3 findOffset(int i)
    {
        switch (i)
        {
            case 0:
                {
                    return new Vector3(size.x, 0, 0);
                }
            case 1:
                {
                    return new Vector3(-size.x, 0, 0);
                }
            case 2:
                {
                    return new Vector3(0, size.y, 0);
                }
            case 3:
                {
                    return new Vector3(0, -size.y, 0);
                }
        }
        return new Vector3(0, 0, 0);
    }
}