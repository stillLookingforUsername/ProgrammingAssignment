using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ObstacleData")]
public class ObstacleData : ScriptableObject
{
  public bool[] blockedTiles = new bool[100];
  public bool IsBlocked(int x, int y)
  {
    return blockedTiles[y * 10 + x];
  }
}
