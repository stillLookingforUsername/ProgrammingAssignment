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

//class representing properties of a single tile
public class GridTile : MonoBehaviour {
    public int x;
    public int y;
    public bool _isBlocked; //if tree player can't move

    #region Up&Down motion
    [Header("TilesUpDownMotion")]
    private Vector3 _originalPosition;  //original world position of tile
    private float _UpDownSpeed;
    private float _UpDownHeight;
    private float _UpDownOffset;
    #endregion

    [Header("MouseHoverHighlight")]
    [SerializeField] private Renderer _tileRenderer;
    [SerializeField] private Color _hoverColor = Color.cyan;

    private void Start()
    {
        _hoverColor = Color.cyan;
    }

    private Color _actualColorOfTile;

    public Vector3 WorldPosition
    {
        get { return _originalPosition;}
    }

    private void Awake()
    {
        _originalPosition = transform.position;

        // Small randomized values per tile
        _UpDownSpeed = Random.Range(0.6f, 1.2f);
        _UpDownHeight = Random.Range(0.05f, 0.15f);
        _UpDownOffset = Random.Range(0f, Mathf.PI * 2f);

        if (_tileRenderer == null)
        {
            _tileRenderer = GetComponent<Renderer>();
        }
        _actualColorOfTile = _tileRenderer.material.color;  //assigning the original color of the tile
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * _UpDownSpeed + _UpDownOffset) * _UpDownHeight;  //for smooth upDown motion
        transform.position = _originalPosition + Vector3.up * yOffset;  //apply offset value without changing originalPosition
    }

    //call this function to change color while mouse hover
    public void SetHover(bool isMouseHovered)
    {
        _tileRenderer.material.color = isMouseHovered ? _hoverColor : _actualColorOfTile;
    }

    public void Pulse() //this function to generate pulse effect like enlarging the tile and srink to original size
    {
        StopAllCoroutines();
        StartCoroutine(PulseOnClick());
    }
    private IEnumerator PulseOnClick()
    {
        float duration = 0.2f;
        float elapsed = 0f;

        Vector3 startScale = transform.localScale;  //original size
        Vector3 targetScale = startScale * 1.5f;    //target size

        //scale up
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale,targetScale, elapsed/duration);
            yield return null;
        }
        elapsed = 0f;
        //scale down
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale,startScale, elapsed/duration);
            yield return null;
        }
        transform.localScale = startScale; //reset tile size
    }
}

