/*using UnityEngine;

public class GridTile : MonoBehaviour
{
    public int x;
    public int y ;
    public bool _isBlocked;
    
    public Vector3 WorldPosition
    {
        get { return transform.position;}
    }
}
*/

using System.Collections;
using UnityEngine;

public class GridTile : MonoBehaviour {
    public int x;
    public int y;
    public bool _isBlocked;

    [Header("TilesUpDownMotion")]
    private Vector3 basePosition;
    private float bobSpeed;
    private float bobHeight;
    private float phaseOffset;

    [Header("MouseHoverHighlight")]
    [SerializeField] private Renderer _tileRenderer;
    [SerializeField] private Color _hoverColor = Color.cyan;

    private Color _actualColorOfTile;

    public Vector3 WorldPosition => basePosition;

    private void Awake()
    {
        basePosition = transform.position;

        // Small randomized values per tile
        bobSpeed = Random.Range(0.6f, 1.2f);
        bobHeight = Random.Range(0.05f, 0.15f);
        phaseOffset = Random.Range(0f, Mathf.PI * 2f);

        if (_tileRenderer == null)
        {
            _tileRenderer = GetComponent<Renderer>();
        }
        _actualColorOfTile = _tileRenderer.material.color;  //assigning the original color of the tile
    }

    private void Update()
    {
        float yOffset =
            Mathf.Sin(Time.time * bobSpeed + phaseOffset) * bobHeight;

        transform.position = basePosition + Vector3.up * yOffset;
    }

    public void SetHover(bool isMouseHovered)
    {
        _tileRenderer.material.color = isMouseHovered ? _hoverColor : _actualColorOfTile;
    }

    public void Pulse()
    {
        StopAllCoroutines();
        StartCoroutine(PulseOnClick());
    }
    private IEnumerator PulseOnClick()
    {
        float duration = 0.2f;
        float elapsed = 0f;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * 1.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale,targetScale, elapsed/duration);
            yield return null;
        }
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale,startScale, elapsed/duration);
            yield return null;
        }
        transform.localScale = startScale;
    }
}

