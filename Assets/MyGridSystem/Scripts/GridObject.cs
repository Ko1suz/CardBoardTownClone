using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GridObject : BaseGridObject
{
    LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();   
        _lineRenderer.positionCount = _positions.Length;
        _lineRenderer.SetPositions(_positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
