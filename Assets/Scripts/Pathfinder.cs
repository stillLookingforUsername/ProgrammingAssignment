using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Pathfinder
{
  public static List<GridTile> FindPath(GridTile start,GridTile end)
  {
    List<PathNode> open = new List<PathNode>();
    HashSet<GridTile> closed = new HashSet<GridTile>();

    PathNode startNode = new PathNode(start);
    open.Add(startNode);

    while(open.Count > 0)
    {
      PathNode current = open[0];
      foreach(var node in open)
      {
        if(node.fCost < current.fCost)
        {
          current = node;
        }
      }
      open.Remove(current);
      closed.Add(current.tile);

      if(current.tile==end)
      {
        return Retrace(current);
      }
      foreach(GridTile neighbour in GetNeighbours(current.tile))
      {
        if(neighbour._isBlocked || closed.Contains(neighbour)) continue;
        int cost = current.gCost + 1;
        PathNode node = open.Find(n => n.tile == neighbour);
        if (node == null)
        {
          node = new PathNode(neighbour);
          node.gCost = cost;
          node.hCost = Mathf.Abs(end.x - neighbour.x) + Mathf.Abs(end.y - neighbour.y);
          node.parent = current;
          open.Add(node);
        }
      }
    }
    return null;
  }

  private static List<GridTile> GetNeighbours(GridTile tile)
  {
    List<GridTile> list = new();
        GridManager gm = GridManager.Instance;

        list.Add(gm.GetTileAtPosition(tile.x + 1, tile.y));
        list.Add(gm.GetTileAtPosition(tile.x - 1, tile.y));
        list.Add(gm.GetTileAtPosition(tile.x, tile.y + 1));
        list.Add(gm.GetTileAtPosition(tile.x, tile.y - 1));

        list.RemoveAll(t => t == null);
        return list;
    }

  private static List<GridTile> Retrace(PathNode node)
    {
        List<GridTile> path = new();
        while (node != null)
        {
            path.Add(node.tile);
            node = node.parent;
        }
        path.Reverse();
        return path;
    }
}