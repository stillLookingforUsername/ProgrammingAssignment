using UnityEngine;

public class GridTile : MonoBehaviour
{
    public int x;
    public int y ;
    public bool _isBlocked;
    
    public Vector3 WorldPosition
    {
        get { return transform.position;}
    }
}
