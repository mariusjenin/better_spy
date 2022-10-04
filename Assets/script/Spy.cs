using System;
using UnityEngine;
using UnityEngine.AI;

namespace script
{
    public class Spy : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface navMeshSurface;
        [SerializeField] private WhiteBoardCameraAware whiteBoard;
        private Camera _camera;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _agent.SetDestination(hit.point);
                }
            }
        }

        public void OnCameraAwareModified()
        {
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            //TODO
        }
    }
}