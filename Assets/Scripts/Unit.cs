using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for grid unit

public class Unit : MonoBehaviour
{
    protected float characterAboveGround = 1.5f; //offset value for character above the ground
    public GridTile currentTile;
    public bool isMoving;

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
        foreach (GridTile tile in path)
        {
            Vector3 startPos = transform.position; //store current posiiton of the character
            Vector3 endPos = tile.WorldPosition + Vector3.up * characterAboveGround;    //calculate end position

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
    }
}

