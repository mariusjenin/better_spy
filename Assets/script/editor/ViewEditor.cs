using System.Collections;
using System.Collections.Generic;
using script;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class ViewEditor : Editor
{
    void OnSceneGUI()
    {
        Handles.color = Color.red;
        FieldOfView cam = (FieldOfView)target;

        var camForward = cam.transform.forward;
        var camPosition = cam.transform.position;
        Vector3 camForwardView = new Vector3(camForward.x, 0, camForward.z);
        Vector3 startDir = Quaternion.AngleAxis(-cam.ViewAngle/2, Vector3.up) * camForwardView;
        Vector3 endDir = Quaternion.AngleAxis(cam.ViewAngle/2, Vector3.up) * camForwardView;

        Handles.DrawWireArc(camPosition, Vector3.up, startDir, cam.ViewAngle, cam.ViewRadius);
        Handles.DrawLine(camPosition, camPosition + startDir * cam.ViewRadius);
        Handles.DrawLine(camPosition, camPosition + endDir * cam.ViewRadius);
        // Debug.Log("CAM POS? " + camPosition);
    }
}
