using UnityEngine;

//this script allows to create as well as to access the every grid
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public float tileSize = 2.2f; //gap between each tile(in world space) not the actual size of each tile or grid 
    public int gridWidth = 10;  //no of tiles on x-axis
    public int gridHeight = 10; //no of tiles on y-axis
    public GameObject tilePrefab;

    public GridTile[,] grid;    //using array to to store all the spawned tiles

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        GenerateGrid();
    }
    
    
    //function to spawn tiles with respect to their position
    private void GenerateGrid()
    {
        grid = new GridTile[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPos = new Vector3(x*tileSize,0,y*tileSize);
                GameObject tile = Instantiate(tilePrefab, worldPos, Quaternion.identity,transform);

                //to store the co-ordinates of x and y to use it later
                GridTile gridTile = tile.GetComponent<GridTile>();
                gridTile.x = x;
                gridTile.y = y;
                grid[x, y] = gridTile;
            }
        }
    }

    //function to get tile at a give position
    public GridTile GetTileAtPosition(int x, int y)
    {
        //statement to check whether a gile exist or not at a given x and y 
        //if not return null 
        if (x<0 || y<0 || x>=gridWidth || y>=gridHeight)
        {
            return null;
        }
        else
        {
            return grid[x, y];
        }
    }
}
