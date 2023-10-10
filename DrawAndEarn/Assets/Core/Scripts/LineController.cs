using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _line;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _edgeCollider2D;
    [SerializeField] private List<Vector2> _touchPositionList;

    [SerializeField] private List<GameObject> _lines;
    [SerializeField] private TextMeshProUGUI _lineCountText;

    private int _lineCount;
    bool canLine;

    private void Start()
    {
        canLine = false;
        _lineCount = 3;
        _lineCountText.text = _lineCount.ToString();
    }
    private void Update()
    {
        if (canLine && _lineCount !=0)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(touchPosition, _touchPositionList[^1]) > .1f)
                {
                    UpdateLine(touchPosition);
                }
            }
        }
        if (_lines.Count!=0 && _lineCount !=0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _lineCount--;
                _lineCountText.text = _lineCount.ToString();
            }
        }
       
    }
    private void CreateLine()
    {
        _line = Instantiate(_linePrefab, Vector2.zero, Quaternion.identity);
        _lines.Add(_line);
        _lineRenderer = _line.GetComponent<LineRenderer>();
        
        _edgeCollider2D = _line.GetComponent<EdgeCollider2D>();

        _touchPositionList.Clear();

        _touchPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _touchPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        _lineRenderer.SetPosition(0, _touchPositionList[0]);
        _lineRenderer.SetPosition(1, _touchPositionList[1]);

        _edgeCollider2D.points = _touchPositionList.ToArray();

    }

    private void UpdateLine(Vector2 getTouchPosition)
    {
        _touchPositionList.Add(getTouchPosition);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount-1,getTouchPosition);
        _edgeCollider2D.points = _touchPositionList.ToArray();
    }
    public void StopLine()
    {
        canLine = false;
    }
    public void StartLine()
    {
        canLine = true;
        _lineCount = 3;
        _lineCountText.text = _lineCount.ToString();
    }

    public void Continue()
    {
        foreach (var line in _lines)
        {
            Destroy(line.gameObject);

        }
        _lines.Clear();
        _lineCount = 3;
        _lineCountText.text = _lineCount.ToString();
    }
}
