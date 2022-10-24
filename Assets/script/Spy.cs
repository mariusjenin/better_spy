using System;
using UnityEngine;
using UnityEngine.AI;

namespace script
{
    public class Spy : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface navMeshSurface;
        private Camera _camera;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _camera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                
                bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
                if (Physics.Raycast(ray, out RaycastHit hit) && !isOverUI)
                {
                    _agent.SetDestination(hit.point);
                }
            }
        }
    }
}