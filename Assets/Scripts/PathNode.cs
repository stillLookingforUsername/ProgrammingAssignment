public class PathNode
{
  public GridTile tile;
  public int gCost;
  public int hCost;

  public PathNode parent;
  public int fCost
  {
    get { return gCost + hCost;}
  }
  public PathNode(GridTile tile)
  {
    this.tile = tile;
  }
}