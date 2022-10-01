using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace script
{
    public struct Bound
    {
        public float upper;
        public float lower;
    };

    public class SpyCamera : MonoBehaviour
    {
        [Range(0,1)]
        public float speed = 1;
        [Range(0,1)]
        public int direction = 1; //1 or -1 -> starting camera rotation clockwise/ anticlockwise
        public float deg = 90;

        private Bound _bound;
        private float _currentAngle;
        private int _currentDirection;

        // Start is called before the first frame update
        void Start()
        {
            _bound.upper = deg / 2;
            _bound.lower = -deg / 2;
            _currentAngle = 0;
            _currentDirection = direction == 1 ? direction : -1;

        }

        // Update is called once per frame
        void Update()
        {
            if((_currentAngle >= _bound.upper)  || (_currentAngle <= _bound.lower))
                _currentDirection *= -1;
                
            _currentAngle += _currentDirection * speed;
            transform.Rotate(0, _currentDirection * speed, 0);
        }
    }
}