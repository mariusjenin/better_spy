using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace script
{
    public class WhiteBoardCameraAware : MonoBehaviour
    {
        [SerializeField] private List<SpyCamera> spyCameras;
        [SerializeField] private NavMeshSurface navMeshSurface;
        [SerializeField] private Spy spy;

        private void Awake()
        {
            // ReinitAware();
        }

        public void ReinitAware()
        {
            foreach (var spyCamera in spyCameras)
            {
                spyCamera.GetComponent<NavMeshModifier>().ignoreFromBuild = true;
            }

            spy.OnCameraAwareModified();
        }

        public void AwareOf(SpyCamera spyCamera, bool aware, bool notifySpy)
        {
            spyCamera.GetComponent<NavMeshModifier>().ignoreFromBuild = aware;
            if(notifySpy) spy.OnCameraAwareModified();
        }
    }
}