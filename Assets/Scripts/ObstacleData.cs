using UnityEngine;

//By using Scriptable Object I stored Obstacles Info
//each value represt whether a tile is blocked or not
//check block and vice-versa

[CreateAssetMenu(menuName = "Scriptable Objects/ObstacleData")]
public class ObstacleData : ScriptableObject
{
  public bool[] blockedTiles = new bool[100];   //10*10 blocks

  //function to check if a tile is blocked or not
  public bool IsBlocked(int x, int y)
  {
    return blockedTiles[y * 10 + x];
  }
}
