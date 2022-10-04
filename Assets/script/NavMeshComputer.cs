using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace script
{
    public class NavMeshComputer : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface navMeshSurface;

        // Update is called once per frame
        void Update()
        {
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
        }
    }

}