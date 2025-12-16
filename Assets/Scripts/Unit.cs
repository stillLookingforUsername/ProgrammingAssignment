using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected float characterAboveGround = 1.5f;
    public GridTile currentTile;
    public bool isMoving;

    //[Header("Animation")]
    //protected Animator animator;

/*
    protected virtual void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    */

    public IEnumerator Move(List<GridTile> path)
    {
        isMoving = true;
        foreach(GridTile tile in path)
        {
            isMoving = true;
            //animator.SetBool("isMovingAnimatorParam", true);

            transform.position = tile.WorldPosition + Vector3.up * characterAboveGround;
            currentTile = tile;
            //PositionCharacterToTile(tile);
            yield return new WaitForSeconds(0.3f);
        }
        isMoving = false;
        //animator.SetBool("isMovingAnimatorParam", false);
    }
}
