using UnityEngine;

public class EnemyAI : Unit, IEnemy
{
    public PlayerUnit player;
    public float enemyAboveGround = 1.5f;

    private void Start()
    {
        currentTile = GridManager.Instance.GetTileAtPosition(0, 0);
        transform.position = currentTile.WorldPosition + Vector3.up * enemyAboveGround;
    }
    private void Update()
    {
        TakeTurn();
    }
    public void TakeTurn()
    {
        if (isMoving || player.isMoving) return;

        GridTile target = GetAdjacentTile();
        if (target == null) return;

        var path = Pathfinder.FindPath(currentTile, target);
        if (path != null)
            StartCoroutine(Move(path));
    }

    GridTile GetAdjacentTile()
    {
        GridManager gm = GridManager.Instance;
        GridTile p = player.currentTile;

        GridTile[] options =
        {
            gm.GetTileAtPosition(p.x + 1, p.y),
            gm.GetTileAtPosition(p.x - 1, p.y),
            gm.GetTileAtPosition(p.x, p.y + 1),
            gm.GetTileAtPosition(p.x, p.y - 1)
        };

        foreach (var t in options)
            if (t != null && !t._isBlocked)
                return t;

        return null;
    }
}
