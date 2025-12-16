using UnityEngine;

//Script for the enemy
public class EnemyAI : Unit, IEnemy
{
    public PlayerUnit player;
    public float enemyAboveGround = 1.5f;

    private void Start()
    {
        currentTile = GridManager.Instance.GetTileAtPosition(0, 0); //spawn player at location (1,1)
        transform.position = currentTile.WorldPosition + Vector3.up * enemyAboveGround; //adding an offset value to make the player stay above the ground
    }
    private void Update()
    {
        TakeTurn();
    }
    public void TakeTurn()
    {
        //dont mve if either the player/enemy is moving
        if (isMoving || player.isMoving)
        {
            return;
        }

        GridTile target = GetAdjacentTile();    //to fine a free tile close to player
        if (target == null)
        {
            return;
        }

        var path = Pathfinder.FindPath(currentTile, target); //to find possible path and move near the player
        if (path != null)
            StartCoroutine(Move(path));
    }

    //to find the available tile close to the player
    private GridTile GetAdjacentTile()
    {
        GridManager gm = GridManager.Instance;
        GridTile p = player.currentTile;

        //check 4 direction around the player
        GridTile[] options =
        {
            gm.GetTileAtPosition(p.x + 1, p.y),
            gm.GetTileAtPosition(p.x - 1, p.y),
            gm.GetTileAtPosition(p.x, p.y + 1),
            gm.GetTileAtPosition(p.x, p.y - 1)
        };

        //give valid tile after finding
        foreach (var t in options)
        {
            if (t != null && !t._isBlocked)
            {
                return t;
            }
        }
        return null; //if valid tile not round just return null;
    }
}
