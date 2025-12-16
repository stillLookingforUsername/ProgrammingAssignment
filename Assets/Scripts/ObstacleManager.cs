using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;
    [SerializeField] private float _heightOffSet = 0.6f;

    private void Start()
    {
        for(int x = 0; x < 10;x++)
        {
            for(int y = 0; y < 10; y++)
            {
                if(obstacleData.IsBlocked(x,y))
                {
                    GridTile tile = GridManager.Instance.GetTileAtPosition(x,y);
                    tile._isBlocked = true;
                    Instantiate(obstaclePrefab, tile.WorldPosition + Vector3.up * _heightOffSet, Quaternion.identity);
                }
            }
        }
    }
}
