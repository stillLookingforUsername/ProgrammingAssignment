using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUnit : Unit
{
    private PlayerInputActions _playerInputActions;
    private Camera _camera;
    public float playerAboveGround = 1.5f;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _camera = Camera.main;
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
        currentTile = GridManager.Instance.GetTileAtPosition(0, 0);
        transform.position = currentTile.WorldPosition + Vector3.up * playerAboveGround;
    }
    
    public void OnClick(InputAction.CallbackContext ctx)
    {
        if(isMoving) return;
        Vector2 mousePos = _playerInputActions.Player.Point.ReadValue<Vector2>();
        Ray ray = _camera.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            GridTile tile = hitInfo.collider.GetComponent<GridTile>();
            if(tile != null && !tile._isBlocked)
            {
                var path = Pathfinder.FindPath(currentTile,tile);
                if(path != null)
                {
                    StartCoroutine(Move(path));
                }
            }
        }
    }
}