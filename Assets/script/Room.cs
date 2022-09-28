using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace script
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Vector2 _posEntrance;
        [SerializeField] private Vector2 _posExit;
        [SerializeField] private List<SpyCamera> cameras;

        public Vector2 PosEntrance
        {
            get => _posEntrance;
            set => _posEntrance = value;
        }

        public Vector2 PosExit
        {
            get => _posExit;
            set => _posExit = value;
        }

        public List<SpyCamera> Cameras
        {
            get => cameras;
            set => cameras = value;
        }
    }
}