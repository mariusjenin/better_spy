using System;
using UnityEngine;

namespace script
{
    public class WalkieTalkie : MonoBehaviour
    {
        private bool _ringing;
        private Vector3 _initTransform;
        [SerializeField] private float speedRotation = 10;
        [SerializeField] private float speedScale = 8;

        private void Start()
        {
            _initTransform = transform.localScale;
        }

        public void Update()
        {
            if (_ringing)
            {
                Transform trsf = transform;
                trsf.rotation= Quaternion.Euler( 0,0, (30f * Mathf.Sin(Time.unscaledTime * speedRotation) + 1.0f));
                trsf.localScale = _initTransform * (0.9f+Mathf.Sin(Time.unscaledTime * speedScale)/8f);
            }
        }

        public void StartRinging()
        {
            _ringing = true;
        }
        public void StopRinging()
        {
            _ringing = false;
            Transform trsf= transform;
            trsf.rotation= Quaternion.Euler( 0,0, 0);
            trsf.localScale = _initTransform;
        }
    }
}
