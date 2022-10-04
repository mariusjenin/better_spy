using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace script
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private float viewRadius;
        [SerializeField] private float viewAngle;
        [SerializeField] private float resolution;
        [SerializeField] private MeshFilter viewMeshFilter;
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private NavMeshSurface navMeshSurface;
        private Mesh _viewMesh;

        public float ViewAngle
        {
            get => viewAngle;
            set => viewAngle = value;
        }

        public float ViewRadius
        {
            get => viewRadius;
            set => viewRadius = value;
        }

        public float Resolution
        {
            get => resolution;
            set => resolution = value;
        }

        public Mesh ViewMesh
        {
            get => _viewMesh;
            set => _viewMesh = value;
        }

        public MeshFilter ViewMeshFilter
        {
            get => viewMeshFilter;
            set => viewMeshFilter = value;
        }
        public struct ViewCastInfo
        {
            private bool _hit;
            private Vector3 _hitPoint;
            private float _distance;
            private float _angle;

            public Vector3 HitPoint
            {
                get => _hitPoint;
                set => _hitPoint = value;
            }


            public ViewCastInfo(bool h, Vector3 hp, float dst, float ang)
            {
                _hit = h;
                _hitPoint = hp;
                _distance = dst;
                _angle = ang;
            }
        }

        //Unity: décalage de 90° comparé au cercle trigo
        public Vector3 DirFromAngle(float angleDeg, bool isGlobal)
        {
            if(!isGlobal)
                angleDeg += transform.eulerAngles.y;

            return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), 0,  Mathf.Cos(angleDeg * Mathf.Deg2Rad));
        }

        ViewCastInfo ViewCast(float angle)
        {
            Vector3 direction = DirFromAngle(angle, true);
            RaycastHit hit;
            if(Physics.Raycast(transform.position, direction, out hit, viewRadius))
            {
                // Debug.Log("HIT");
                return new ViewCastInfo(true, hit.point, hit.distance, angle);
            }
            else
            {
                return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, angle);
            }
        }

        void DrawFieldOfView()
        {
            int count = Mathf.RoundToInt(viewAngle * resolution);   //nb de segments;
            float angleStep = viewAngle / count; // degrés par segments;
            float angle;
        
            List<Vector3> castPoints = new List<Vector3> ();

            for(int i = 0; i < count; i ++)
            {
                angle = transform.eulerAngles.y - viewAngle/2 + angleStep * i;
                // Debug.DrawLine(transform.position, transform.position +  DirFromAngle(angle, true) * viewRadius, Color.blue);

                ViewCastInfo vCast = ViewCast(angle);
                castPoints.Add(vCast.HitPoint); //pts en coordonnées monde (besoin d'être relatifs (car le mesh sera enfant de la camera))
            }

            int vertexCount = castPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount-2) * 3];

            vertices[0] = Vector3.zero;

            for(int i = 0; i < vertexCount-1; i ++)
            {
                vertices[i+1] = transform.InverseTransformPoint(castPoints[i]);
                if(i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            _viewMesh.Clear();
            _viewMesh.vertices = vertices;
            _viewMesh.triangles = triangles;
            _viewMesh.RecalculateNormals();
        }

        void Start() {
            _viewMesh = new Mesh();
            _viewMesh.name = "View Mesh";

            viewMeshFilter.mesh = _viewMesh;
            meshCollider.sharedMesh = _viewMesh;
            meshCollider.convex = true;
        }

        void LateUpdate()
        {
            meshCollider.convex = false;
            DrawFieldOfView();
        }
    }
}
