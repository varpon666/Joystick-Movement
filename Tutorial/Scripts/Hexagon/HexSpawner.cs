using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSpawner : MonoBehaviour
{
    [SerializeField] private Hex _hexagon;
    [SerializeField] private Hex _hexagonSide;
    [SerializeField] private Hex _hexagonTop;
    [SerializeField] private Hex _hexagonDownRightCorner;
    [SerializeField] private Hex _hexagonTopRightCorner;
    [SerializeField] private Hex _hexagonTopLeftCorner;

    [SerializeField] private Transform _parent;

    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    [SerializeField] private float _xOffset;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _rowsOffset;

    private Vector3 _startPosition;
    private Vector3 _currentPosition;

    private void Awake()
    {
        _startPosition = _parent.position;
        _currentPosition = _startPosition;
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector3 bottom = new Vector3(_startPosition.x - _rowsOffset, _startPosition.y, _startPosition.z);
        Vector3 top = bottom;

        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                Instantiate(_hexagon, _currentPosition, Quaternion.identity, _parent);
                _currentPosition = new Vector3(_currentPosition.x + _xOffset, 0f, _currentPosition.z);
            }

            if (i % 2 != 0 || i == 1)
            {
                Instantiate(_hexagonSide, new Vector3(_currentPosition.x, 0f, _currentPosition.z), Quaternion.identity, _parent);

                _startPosition = new Vector3(_startPosition.x + _rowsOffset, _startPosition.y, _startPosition.z);
            }
            else
            {
                Instantiate(_hexagonSide, new Vector3(_startPosition.x - _xOffset, 0f, _currentPosition.z), Quaternion.Euler(0f, 180f, 0f), _parent);

                _startPosition = new Vector3(_startPosition.x - _rowsOffset, +_startPosition.y, _startPosition.z);
            }

            _currentPosition = new Vector3(_startPosition.x, 0f, _currentPosition.z + _zOffset);
        }

        // set bottom row

        for (int b = 0; b <= _rows; b++)
        {
            if(b == _rows)
            {
                Instantiate(_hexagonDownRightCorner, new Vector3(bottom.x, 0f, bottom.z - _zOffset), Quaternion.Euler(0f, 180f, 0f), _parent);
                break;
            }

            Instantiate(_hexagonTop, new Vector3(bottom.x, 0f, bottom.z - _zOffset), Quaternion.identity, _parent);

            bottom = new Vector3(bottom.x + _xOffset, 0f, bottom.z);
        }

        //set top row

        if (_columns % 2 == 0)
            top = new Vector3(top.x - _rowsOffset, _currentPosition.y, _currentPosition.z);
        else
            top = new Vector3(top.x, _currentPosition.y, _currentPosition.z);

        for (int t = 0; t <= _rows; t++)
        {
            if(_columns % 2 != 0)
            {
                if(t == _rows)
                {
                    Instantiate(_hexagonTopRightCorner, new Vector3(top.x, 0f, top.z), Quaternion.identity, _parent);
                    break;
                }
            }

            else if(_columns % 2 == 0)
            {
                if(t == 0)
                {
                    Instantiate(_hexagonTopLeftCorner, new Vector3(top.x, 0f, top.z), Quaternion.Euler(0f, 180f, 0f), _parent);

                    top = new Vector3(top.x + _xOffset, _currentPosition.y, _currentPosition.z);
                    continue;
                }
            }

            Instantiate(_hexagonTop, new Vector3(top.x, 0f, top.z), Quaternion.Euler(0f, 180f, 0f), _parent);

            top = new Vector3(top.x + _xOffset, 0f, top.z);
        }
    }
}
