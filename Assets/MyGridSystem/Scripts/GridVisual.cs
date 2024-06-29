using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
public class GridVisual : GridObject
{
    LineRenderer _lineRenderer;
    public bool createGridVisual = false;
    public Material lineMat;
    // Start is called before the first frame update
    void Start()
    {
        //_lineRenderer = GetComponent<LineRenderer>();
        //_lineRenderer.positionCount = _positions.Length;
        //_lineRenderer.SetPositions(_positions);
        CreateGrids(createGridVisual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrids(bool createGrid)
    {
        if (createGrid)
        {
            this.gameObject.AddComponent<LineRenderer>();
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.positionCount = _positions.Length;
            _lineRenderer.SetPositions(_positions);

            Material cloneMat = Instantiate(lineMat);
            _lineRenderer.material = cloneMat;
        }
    }
}
