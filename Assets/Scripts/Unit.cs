using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected float characterAboveGround = 1.5f;
    public GridTile currentTile;
    public bool isMoving;

    public IEnumerator Move(List<GridTile> path)
    {
        isMoving = true;
        foreach(GridTile tile in path)
        {
            transform.position = tile.WorldPosition + Vector3.up * characterAboveGround;
            currentTile = tile;
            yield return new WaitForSeconds(0.3f);
        }
        isMoving = false;
    }
}
