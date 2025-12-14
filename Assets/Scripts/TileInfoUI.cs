using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileInfoUI : MonoBehaviour
{
    public TextMeshProUGUI tileInfoText;
    private PlayerInputActions _playerInputActions;
    private Camera _mainCamera;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
    }
    private void OnDisable()
    {
        _playerInputActions.Disable();
    }

    private void Update()
    {
        Vector2 mousePos = _playerInputActions.Player.Point.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            GridTile tile = hitInfo.collider.GetComponent<GridTile>();
            if(tile != null)
            {
                tileInfoText.text = $"Tile Position: ({tile.x}, {tile.y})\n";
            }
        }
    }
}
