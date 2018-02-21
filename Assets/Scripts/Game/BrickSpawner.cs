using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BrickSpawner : MonoBehaviour {

    public Tile brick;
    public Vector2Int size;
    public Tilemap walls; // For finding where walls are.
    public Tilemap bricks;
    public int brickOmit_1;
    public int brickOmit_2;
    public int brickInclude_1;

	// Use this for initialization
	void Start () {
        Vector3Int[] tilePositions = new Vector3Int[size.x * size.y];
        Tile[] tileArray = new Tile[tilePositions.Length];

        // Loop through tileArray and place brick tiles into the tileArray.
        for (int i = 0; i < tilePositions.Length; i++) {
            int x = i % size.x;
            int y = i / size.x;
            tilePositions[i] = new Vector3Int(x, y, 0);

            // Don't spawn bricks on the corners where players spawn, on walls, or in random splotches.
            if (x <= 1 && y <= 1) { // Bottom left.
                continue;
            } else if (x >= size.x - 2 && y <= 1) { // Botom right.
                continue;
            } else if (x <= 1 && y >= size.y - 2) { // Top left.
                continue;
            } else if (x >= size.x - 2 && y >= size.y - 2) { // Top right.
                continue;
            } else if (walls.HasTile(tilePositions[i] + Vector3Int.RoundToInt(bricks.tileAnchor))) {  // Don't spawn on walls.
                //Debug.Log("Wall here! " + tilePositions[i]);
                continue;
            } else if (i % brickOmit_1 == 0 || i % brickOmit_2 == 0) {  // Logic for randomly omiting bricks in the center.
                if (i % brickInclude_1 != 0) {
                    continue;
                }
            }

            // Spawn bricks everywhere else.
            tileArray[i] = brick;
            //Debug.Log("Brick index: " + i);
            //Debug.Log("tilePosition: " + tilePositions[i]);
        }

        // Populate the tilemap with the tileArray
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTiles(tilePositions, tileArray);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
