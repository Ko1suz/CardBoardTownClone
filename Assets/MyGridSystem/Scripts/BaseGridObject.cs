using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGridObject : MonoBehaviour
{
    [SerializeField] protected bool showDebug = true;
    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    }
    [SerializeField] _axis axis = new _axis();
    public enum _initiator
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon,
    }
    [SerializeField] protected _initiator initiator = new _initiator();

    protected int _initiatorPointAmount;
    private Vector3[] _initiatorPoints;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    private float _initialRotation;
    [SerializeField] protected float gridSize;

    protected Vector3[] _positions;

    protected void Awake()
    {
        SetInitiatorPointAmount();
        _positions = new Vector3[_initiatorPointAmount + 1];

        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _positions[i] = _rotateVector * gridSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        _positions[_initiatorPointAmount] = _positions[0];
    }

    protected void OnDrawGizmos()
    {
        if (showDebug)
        {
            for (int i = 0; i < _initiatorPointAmount; i++)
            {
                Gizmos.color = Color.white;
                Matrix4x4 rotatinMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
                Gizmos.matrix = rotatinMatrix;
                if (i < _initiatorPointAmount - 1)
                {
                    Gizmos.DrawLine(_initiatorPoints[i], _initiatorPoints[i + 1]);
                }
                else
                {
                    Gizmos.DrawLine(_initiatorPoints[i], _initiatorPoints[0]);
                }
            }
        }
    }
    public void SetInitiatorPointAmount()
    {
        switch (initiator)
        {
            case _initiator.Triangle:
                _initiatorPointAmount = 3;
                _initialRotation = 0;
                break;
            case _initiator.Square:
                _initiatorPointAmount = 4;
                _initialRotation = 45;
                break;
            case _initiator.Pentagon:
                _initiatorPointAmount = 5;
                _initialRotation = 36;
                break;
            case _initiator.Hexagon:
                _initiatorPointAmount = 6;
                _initialRotation = 30;
                break;
            case _initiator.Heptagon:
                _initiatorPointAmount = 7;
                _initialRotation = 25.7148f;
                break;
            case _initiator.Octagon:
                _initiatorPointAmount = 8;
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
