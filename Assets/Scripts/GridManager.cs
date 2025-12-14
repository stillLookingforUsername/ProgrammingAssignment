using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int gridWidth = 10;
    public int gridHeight = 10;
    public GameObject tilePrefab;

    public GridTile[,] grid;

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
    
    private void GenerateGrid()
    {
        grid = new GridTile[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.identity,transform);
                tile.name = $"Tile_{x}_{y}";
                GridTile gridTile = tile.GetComponent<GridTile>();
                gridTile.x = x;
                gridTile.y = y;
                grid[x, y] = gridTile;
            }
        }
    }

    public GridTile GetTileAtPosition(int x, int y)
    {
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
