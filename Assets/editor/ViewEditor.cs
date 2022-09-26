using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class ViewEditor : Editor
{
    void OnSceneGUI()
    {
        Handles.color = Color.red;
        FieldOfView cam = (FieldOfView)target;

        Vector3 camForwardView = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        Vector3 startDir = Quaternion.AngleAxis(-cam.viewAngle/2, Vector3.up) * camForwardView;
        Vector3 endDir = Quaternion.AngleAxis(cam.viewAngle/2, Vector3.up) * camForwardView;

        Handles.DrawWireArc(cam.transform.position, Vector3.up, startDir, cam.viewAngle, cam.viewRadius);
        Handles.DrawLine(cam.transform.position, cam.transform.position + startDir * cam.viewRadius);
        Handles.DrawLine(cam.transform.position, cam.transform.position + endDir * cam.viewRadius);
        // Debug.Log("CAM POS? " + cam.transform.position);
    }
}
