using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public float resolution;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 hitPoint;
        public float distance;
        public float angle;

        public ViewCastInfo(bool h, Vector3 hp, float dst, float ang)
        {
            hit = h;
            hitPoint = hp;
            distance = dst;
            angle = ang;
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
            castPoints.Add(vCast.hitPoint); //pts en coordonnées monde (besoin d'être relatifs (car le mesh sera enfant de la camera))
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

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    void Start() {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";

        viewMeshFilter.mesh = viewMesh;
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }
}
