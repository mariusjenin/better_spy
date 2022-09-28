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
        public float speed = 10;
        public float deg = 90;

        private Bound _bound;
        private Quaternion _targetQuat;
        private Vector3 _baseRot;

        // Start is called before the first frame update
        void Start()
        {
            var rotation = transform.rotation;
            _targetQuat = Quaternion.Euler(rotation.eulerAngles + new Vector3(0, deg / 2, 0));
            _bound.upper = (rotation.eulerAngles.y + deg / 2) % 360;
            _bound.lower = (rotation.eulerAngles.y - deg / 2 + 360) % 360;

            _baseRot = rotation.eulerAngles;
            Debug.Log("upper " + _bound.upper + ", lower " + _bound.lower);
        }

        // Update is called once per frame
        void Update()
        {
            var rotation = transform.rotation;
            float step = speed * Time.deltaTime;
            Vector3 currentRot = rotation.eulerAngles;
            // current_rot.Set(current_rot.x % 360, current_rot.y % 360, current_rot.z % 360);

            // Debug.Log("rot" + current_rot.y );
            if (Math.Abs(currentRot.y - _bound.upper) <= 0.001f || Math.Abs(currentRot.y - _bound.lower) <= 0.001f)
            {
                // Debug.Log("eee");

                deg *= -1;

                // Vector3 new_rot = current_rot + new Vector3(0, deg, 0);
                // Debug.Log("current rot " + current_rot + "deg " + deg + "newRot: " + new_rot);
                // target_quat = Quaternion.Euler(new_rot);
                _targetQuat = Quaternion.Euler(new Vector3(_baseRot.x, _baseRot.y + deg / 2, _baseRot.z));
                Debug.Log(_targetQuat.eulerAngles);
            }

            // Debug.Log("target quat " + target_quat.eulerAngles);

            transform.rotation = Quaternion.RotateTowards(rotation, _targetQuat, step);
        }
    }
}