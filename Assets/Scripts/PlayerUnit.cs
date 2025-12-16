using UnityEngine;
using UnityEngine.InputSystem;

//class to moves a grid using mouse click
//inherit from Unit class
public class PlayerUnit : Unit
{
    private PlayerInputActions _playerInputActions;
    private Camera _camera;
    public float playerAboveGround = 1.5f;  //player height offset with respect to the ground
    private GridTile _tile;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _camera = Camera.main;
        _tile = GetComponent<GridTile>();
    }
    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Click.performed += OnClick; 
    }
    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
        _playerInputActions.Player.Click.performed -= OnClick;
    }

    private void Start()
    {
        currentTile = GridManager.Instance.GetTileAtPosition(0, 0); //set starting tile
        transform.position = currentTile.WorldPosition + Vector3.up * playerAboveGround; //set height of player
    }
    
    //trigger function when player click
    public void OnClick(InputAction.CallbackContext ctx)
    {
        if(isMoving) return;
        Vector2 mousePos = _playerInputActions.Player.Point.ReadValue<Vector2>();
        Ray ray = _camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            GridTile tile = hitInfo.collider.GetComponent<GridTile>();  //store grid tile when we click a grid

            //return if tile is blocked,invalid,null
            if (tile == null || tile._isBlocked || tile == currentTile)
            {
                return;
            }
            //tiles pulse when clicked
            tile.Pulse();

            //return a path from Pathfinder.FindPath() & store it in path
            var path = Pathfinder.FindPath(currentTile, tile);

            if (path != null)
            {
                StartCoroutine(Move(path));
            }
        }
    }
}