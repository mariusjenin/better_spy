using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace script
{
    public class WhiteBoardCameraAware : MonoBehaviour
    {
        [SerializeField] private List<SpyCamera> spyCameras;
        [SerializeField] private NavMeshSurface navMeshSurface;
        [SerializeField] private int numCamDeactivatedMax = 1;
        [SerializeField] private float timingMax;
        [SerializeField] private WalkieTalkie walkieTalkie;
        [SerializeField] private Button buttonPlay;
        private List<SpyCamera> _camDeactivated;
        private float _timing;
        private bool _inPause;

        private void Awake()
        {
            ReinitAware();
            _timing = timingMax;
            _inPause = false;
            _camDeactivated = new List<SpyCamera>();
        }

        private void Update()
        {
            if (!_inPause)
            {
                _timing -= Time.deltaTime;
                if (_timing <= 0)
                {
                    _inPause = true;
                    Time.timeScale = 0;
                    DisplayUIAction();
                }
            }
        }

        private void DisplayUIAction()
        {
            buttonPlay.interactable = true;
            walkieTalkie.StartRinging();
        }

        private void HideUIAction()
        {
            buttonPlay.interactable = false;
            walkieTalkie.StopRinging();
        }

        public void HelpSpyDone()
        {
            _inPause = false;
            _timing = timingMax;
            Time.timeScale = 1;
            HideUIAction();
        }

        private void ReinitAware()
        {
            foreach (var spyCamera in spyCameras)
            {
                spyCamera.GetComponent<NavMeshModifier>().ignoreFromBuild = false;
            }

            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
        }

        public void Deactivate(SpyCamera spyCamera)
        {
            if (!_camDeactivated.Contains(spyCamera))
            {
                Debug.Log(_camDeactivated.Count);
                if (_camDeactivated.Count >= numCamDeactivatedMax)
                {
                    Activate(_camDeactivated[0]);
                }
                _camDeactivated.Add(spyCamera);
                spyCamera.GetComponent<NavMeshModifier>().ignoreFromBuild = true;
                navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
                spyCamera.OnChangeActive();
            }
        }
        
        
        public void Activate(SpyCamera spyCamera)
        {
            if (_camDeactivated.Contains(spyCamera))
            {
                _camDeactivated.Remove(spyCamera);
                spyCamera.GetComponent<NavMeshModifier>().ignoreFromBuild = false;
                navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
                spyCamera.OnChangeActive();
            }
        }
    }
}