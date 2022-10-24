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
        [SerializeField] private Material materialActive;
        [SerializeField] private Material materialInactive;
        [SerializeField] private WhiteBoardCameraAware whiteBoard;
        private Camera _camera;
        private float SpyCameraSpeedConstant = 40f;
        [Range(0,1)]
        public float speed = 1;
        [Range(0,1)]
        public int direction = 1; //1 or -1 -> starting camera rotation clockwise/ anticlockwise
        public float deg = 90;

        private Bound _bound;
        private float _currentAngle;
        private int _currentDirection;
        private bool _isActive;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _isActive = true;
            _camera = Camera.main;
            _bound.upper = deg / 2;
            _bound.lower = -deg / 2;
            _currentAngle = 0;
            _currentDirection = direction == 1 ? direction : -1;
        }

        private void Start()
        {
            _meshRenderer = gameObject.transform.Find("ViewVisualization").GetComponent<MeshRenderer>();
            _meshRenderer.material = _isActive ? materialActive : materialInactive;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                
                bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                if (Physics.Raycast(ray, out RaycastHit hit) && !isOverUI)
                {
                    if (gameObject == hit.collider.gameObject)
                    {
                        ToggleActive(true);
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if((_currentAngle >= _bound.upper)  || (_currentAngle <= _bound.lower))
                _currentDirection *= -1;
                
            _currentAngle += _currentDirection * speed * Time.fixedDeltaTime * SpyCameraSpeedConstant;
            transform.Rotate(0, _currentDirection * speed * Time.fixedDeltaTime * SpyCameraSpeedConstant, 0);
        }

        public void ToggleActive(bool callWhiteBoard = false)
        {
            if(callWhiteBoard)
            {
                if (_isActive)
                {
                    whiteBoard.Deactivate(this);
                }
                else
                {
                    whiteBoard.Activate(this);
                }
            }
        }

        public void OnChangeActive()
        {
            _isActive = !_isActive;
            _meshRenderer.material = _isActive?materialActive:materialInactive;
        }
    }
}