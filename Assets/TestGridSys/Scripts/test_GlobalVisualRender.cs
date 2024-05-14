using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_GlobalVisualRender : MonoBehaviour 
{
    [SerializeField] protected bool showDebug = true;
    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    }
    [SerializeField] _axis axis = new _axis();
    public enum _shape
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon,
    }
    [SerializeField] protected _shape shape = new _shape();

    protected int _shapePointAmount;
    private Vector3[] _shapePoints;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initialRotation;
    [SerializeField] protected float gridSize;

    protected Vector3[] _positions;

    public _shape Shape { get => shape; set => shape = value; }
    public float GridSize { get => gridSize; set => gridSize = value; }

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

    private void OnDrawGizmos()
    {
        if (showDebug)
        {
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
                Gizmos.color = Color.white;
                Matrix4x4 rotatinMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
                Gizmos.matrix = rotatinMatrix;
                if (i < _shapePointAmount - 1)
                {
                    Gizmos.DrawLine(_shapePoints[i], _shapePoints[i + 1]);
                }
                else
                {
                    Gizmos.DrawLine(_shapePoints[i], _shapePoints[0]);
                }
            }
        }
    }
    public void SetInitiatorPointAmount()
    {
        switch (shape)
        {
            case _shape.Triangle:
                _shapePointAmount = 3;
                _initialRotation = 0;
                break;
            case _shape.Square:
                _shapePointAmount = 4;
                _initialRotation = 0; //45
                break;
            case _shape.Pentagon:
                _shapePointAmount = 5;
                _initialRotation = 36;
                break;
            case _shape.Hexagon:
                _shapePointAmount = 6;
                _initialRotation = 30;
                break;
            case _shape.Heptagon:
                _shapePointAmount = 7;
                _initialRotation = 25.7148f;
                break;
            case _shape.Octagon:
                _shapePointAmount = 8;
                _initialRotation = 22.5f;
                break;
            default:
                break;
        }

        switch (axis)
        {
            case _axis.XAxis:
                _rotateVector = new Vector3(1, 0, 0);
                _rotateAxis = new Vector3(0, 0, 1);
                break;
            case _axis.YAxis:
                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0);
                break;
            case _axis.ZAxis:
                _rotateVector = new Vector3(0, 0, 1);
                _rotateAxis = new Vector3(0, 1, 0);
                break;
            default:
                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0);
                break;
        }
    }
}
