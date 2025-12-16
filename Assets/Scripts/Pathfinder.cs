using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

//path finding class to find path between to grid position
public class Pathfinder
{
  public static List<GridTile> FindPath(GridTile start,GridTile end)
  {
    List<PathNode> open = new List<PathNode>(); //stores list of nodes to check
    HashSet<GridTile> closed = new HashSet<GridTile>(); //stores list of tile that are checked

    PathNode startNode = new PathNode(start);   //assign new starting node
    open.Add(startNode);    //add to the list
    
    //loop through all the nodes to be cheched
    while(open.Count > 0)
    {
      PathNode current = open[0]; //first node with low cost
      foreach(var node in open)
      {
        if(node.fCost < current.fCost)
        {
          current = node;
        }
      }
      open.Remove(current);
      closed.Add(current.tile);

      //if current tile = target position reset the path
      if(current.tile==end)
      {
        return Retrace(current);
      }
      //loop to check all nearby tiles
      foreach(GridTile neighbour in GetNeighbours(current.tile))
      {
       //skip blocked tiles or already checked tiles
       if (neighbour._isBlocked || closed.Contains(neighbour))
       {
          continue;
       }
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
    return null; //if now path found
  }

   //function to return all the valid tiles(up,down,left,right)
  private static List<GridTile> GetNeighbours(GridTile tile)
  {
    List<GridTile> list = new();
        GridManager gm = GridManager.Instance;

        list.Add(gm.GetTileAtPosition(tile.x + 1, tile.y)); //right
        list.Add(gm.GetTileAtPosition(tile.x - 1, tile.y)); //left
        list.Add(gm.GetTileAtPosition(tile.x, tile.y + 1)); //up
        list.Add(gm.GetTileAtPosition(tile.x, tile.y - 1)); //down

        list.RemoveAll(t =>
        {
            if (t == null)
            {
                Debug.Log("Removed a null entry from list");
                return true;
            }
            return false;
        });
        return list;
    }

    //Reconstructs the path by following parent nodes from the end back to the start.
    private static List<GridTile> Retrace(PathNode node)
    {
        List<GridTile> path = new();
        //walk backward through parent until start is reached
        while (node != null)
        {
            path.Add(node.tile);
            node = node.parent;
        }
        path.Reverse(); //to reverse the path so it goes from start to end
        return path;
    }
}