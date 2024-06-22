using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class test_SquareGrid : test_BaseGrid
{
    [Header("Görsel deðerler")]
   
    protected int _shapePointAmount;
    private Vector3[] _shapePoints;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initialRotation;
    private float gridSize = .5f;
    public override float GetGridSizeMultiplier()
    {
        return gridSize;
    }

    protected Vector3[] _positions;

    private Vector3 originPosition;

    public test_SquareGrid(int x, int y, int z, float baseGridSize, bool showDebug = false) : base(x, y, z, baseGridSize)
    {
        this.showDebug = showDebug;
        this.gridSize = baseGridSize/2;
        isSquareGrid = true;
        //DrawDebugLines(new Vector3(x, y, z));
    }

    public override void CreateLineRenderer(Vector3 worldPos, Material lineMat)
    {
        SetInitiatorPointAmount();
        _positions = new Vector3[_shapePointAmount + 1];

        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;
        for (int i = 0; i < _shapePointAmount; i++)
        {
            _positions[i] = _rotateVector * gridSize;
            _rotateVector = Quaternion.AngleAxis(360 / _shapePointAmount, _rotateAxis) * _rotateVector;
        }
        _positions[_shapePointAmount] = _positions[0];


        GameObject LineRendererObj = new GameObject();
        LineRendererObj.transform.position = worldPos;
        LineRenderer _lineRenderer = LineRendererObj.AddComponent<LineRenderer>();

        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.positionCount = _positions.Length;
        _lineRenderer.SetPositions(_positions);
        _lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        //_lineRenderer.numCapVertices = 90;
        _lineRenderer.loop = true;

        _lineRenderer.material = lineMat;

    }

    public override Vector3 GetWorldPosition()
    {
        return new Vector3(x, y, (z * 2) + 1) * baseGridSize + originPosition;
    }
    public override void DrawDebugLines(Vector3 worldPos)
    {
        if (showDebug)
        {

            SetInitiatorPointAmount();
            _shapePoints = new Vector3[_shapePointAmount + 1];
            _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;
            for (int i = 0; i < _shapePointAmount; i++)
            {
                _shapePoints[i] = _rotateVector * gridSize; // / 1.414f;
                _rotateVector = Quaternion.AngleAxis(360 / _shapePointAmount, _rotateAxis) * _rotateVector;
            }
            for (int i = 0; i < _shapePointAmount; i++)
            {
                if (i < _shapePointAmount - 1)
                {
                    Debug.DrawLine(_shapePoints[i] + worldPos, _shapePoints[i + 1] + worldPos, Color.white, 1000f);
                }
                else
                {
                    Debug.DrawLine(_shapePoints[i] + worldPos, _shapePoints[0] + worldPos, Color.white, 1000f);
                }
            }
            UtilsClass.CreateWorldText(("x="+worldPos.x+" y="+ worldPos.y+ " z="+ worldPos.z), null, worldPos, 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
        }
    }
    public void SetInitiatorPointAmount()
    {
        // kare
        _shapePointAmount = 4;
        _initialRotation = 0; //45  

        //// sekizgen
        //_shapePointAmount = 8;
        //_initialRotation = 22.5f;  

        _rotateVector = new Vector3(0, 0, 1);
        _rotateAxis = new Vector3(0, 1, 0);

       
    }
}
