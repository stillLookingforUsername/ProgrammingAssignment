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
    //[SerializeField] protected Animator animator;

/*
    protected virtual void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }
    */

    #region ToRoateCharacter Towards the direction
    [SerializeField] protected float turnSpeed = 10f;

    protected void RotateCharacter(Vector3 direction)
    {
        if(direction == Vector3.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(
        transform.rotation,
        targetRotation,
        Time.deltaTime * turnSpeed
        );
    }
    #endregion

    public IEnumerator Move(List<GridTile> path)
    {
        isMoving = true;
        //animator.SetBool("isMovingAnimatorParam", true);
        //animator.SetBool("isMovingAnimatorParam", isMoving);
        /*
        foreach(GridTile tile in path)
        {
            isMoving = true;

            //snap to tile offset for y-axis
            transform.position = tile.WorldPosition + Vector3.up * characterAboveGround;
            currentTile = tile;
            //endregion

            //PositionCharacterToTile(tile);
            yield return new WaitForSeconds(0.3f);
        }
        */
         foreach (GridTile tile in path)
        {
        Vector3 startPos = transform.position;
        Vector3 endPos = tile.WorldPosition + Vector3.up * characterAboveGround;

        Vector3 moveDir = (endPos - startPos).normalized;

        float t = 0f;
        float moveDuration = 0.25f;

        while (t < 1f)
        {
            RotateCharacter(moveDir);

            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += Time.deltaTime / moveDuration;
            yield return null;
        }

        // Snap to exact tile position
        transform.position = endPos;

        currentTile = tile;
        yield return new WaitForSeconds(0.3f);
        }


        isMoving = false;
        //animator.SetBool("isMovingAnimatorParam", false);
        //animator.SetBool("isMovingAnimatorParam", isMoving);
        //animator.SetBool("isMovingAnimatorParam", false);
    }
}
