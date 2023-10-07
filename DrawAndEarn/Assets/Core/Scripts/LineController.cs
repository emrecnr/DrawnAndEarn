using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject line;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private List<Vector2> touchPositionList;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(touchPosition, touchPositionList[^1]) >.1f)
            {
                UpdateLine(touchPosition);
            }
        }
    }
    private void CreateLine()
    {
        line = Instantiate(linePrefab, Vector2.zero, Quaternion.identity);
        lineRenderer = line.GetComponent<LineRenderer>();
        
        edgeCollider2D = line.GetComponent<EdgeCollider2D>();

        touchPositionList.Clear();

        touchPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        touchPositionList.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        lineRenderer.SetPosition(0, touchPositionList[0]);
        lineRenderer.SetPosition(1, touchPositionList[1]);

        edgeCollider2D.points = touchPositionList.ToArray();

    }

    private void UpdateLine(Vector2 getTouchPosition)
    {
        touchPositionList.Add(getTouchPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1,getTouchPosition);
        edgeCollider2D.points = touchPositionList.ToArray();
    }

}
