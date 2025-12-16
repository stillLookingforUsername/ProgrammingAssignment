using UnityEngine;

//spawn obstacles on grid
public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;   //stores blocked tiles
    public GameObject obstaclePrefab;   
    [SerializeField] private float _heightOffSet = 0.6f; //height offset with respect to grid tile

    private void Start()
    {
        for(int x = 0; x < 10;x++)
        {
            for(int y = 0; y < 10; y++)
            {
                //check each tile whether blocked or not
                if(obstacleData.IsBlocked(x,y))
                {
                    //get grid position from gridmanager
                    GridTile tile = GridManager.Instance.GetTileAtPosition(x,y);
                    tile._isBlocked = true; //if marked true then blocked character can't walk on it
                    Instantiate(obstaclePrefab, tile.WorldPosition + Vector3.up * _heightOffSet, Quaternion.identity); //spawn the cube obstacles
                }
            }
        }
    }
}

