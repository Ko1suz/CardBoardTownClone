using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_OctagonGrid : test_BaseGrid
{
    [Header("Görsel deðerler")]

    protected int _shapePointAmount;
    private Vector3[] _shapePoints;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initialRotation;
    private float gridSize = 1;

    protected Vector3[] _positions;


    public test_OctagonGrid(int x, int y, int z, bool showDebug = false) : base(x, y, z)
    {
        this.showDebug = showDebug;
        DrawDebugLines(x, y, z);
    }

    private void OnEnable()
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
    }

    public void DrawDebugLines(int x, int y, int z)
    {

        if (showDebug)
        {
            Debug.Log("DebugDrawLines Çalýþýtý");
            SetInitiatorPointAmount();
            _shapePoints = new Vector3[_shapePointAmount + 1];
            _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;
            for (int i = 0; i < _shapePointAmount; i++)
            {
                _shapePoints[i] = _rotateVector * gridSize;
                _rotateVector = Quaternion.AngleAxis(360 / _shapePointAmount, _rotateAxis) * _rotateVector;
            }
            for (int i = 0; i < _shapePointAmount; i++)
            {
                if (i < _shapePointAmount - 1)
                {
                    Debug.DrawLine(_shapePoints[i] + new Vector3(x, y, z), _shapePoints[i + 1] + new Vector3(x, y, z), Color.white, 1000f);
                }
                else
                {
                    Debug.DrawLine(_shapePoints[i] + new Vector3(x, y, z), _shapePoints[0] + new Vector3(x, y, z), Color.white, 1000f);
                }
            }
            UtilsClass.CreateWorldText(("x=" + x + " y=" + y + " z=" + z), null, new Vector3(x, y, z), 20, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
        }
    }
    public void SetInitiatorPointAmount()
    {
        //// kare
        //_shapePointAmount = 4;
        //_initialRotation = 0; //45  

        // sekizgen
        _shapePointAmount = 8;
        _initialRotation = 22.5f;

        _rotateVector = new Vector3(0, 0, 1);
        _rotateAxis = new Vector3(0, 1, 0);


    }
}
