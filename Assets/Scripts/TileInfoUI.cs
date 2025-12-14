using TMPro;
using UnityEngine;

public class TileInfoUI : MonoBehaviour
{
    public TextMeshProUGUI tileInfoText;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            GridTile tile = hitInfo.collider.GetComponent<GridTile>();
            if(tile != null)
            {
                tileInfoText.text = $"Tile Position: ({tile.x}, {tile.y})";
            }
        }
    }
}
