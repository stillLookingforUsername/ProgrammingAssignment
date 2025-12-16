//class that represet a single step in pathfinding process
public class PathNode
{
  public GridTile tile; //declare a gridTile from GridTile class
  public int gCost; //cost frm reach start to this tile (public GridTile tile);
  public int hCost; //cost frm this tile to target tile

  public PathNode parent;   //declare a pathNode

  //getter to get total cost
  public int fCost
  {
    get { return gCost + hCost;}
  }
  //function to return a reference  to the grid tile
  public PathNode(GridTile tile)
  {
    this.tile = tile;
  }
}