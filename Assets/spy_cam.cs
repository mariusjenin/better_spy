using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public struct Bound
{
    public float upper;
    public float lower;
};


public class spy_cam : MonoBehaviour
{
    public float speed = 10;
    public float deg = 90;

    Bound bound;
    Quaternion target_quat;
    Vector3 base_rot;

    // Start is called before the first frame update
    void Start()
    {
        target_quat = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, deg/2, 0));
        bound.upper = (transform.rotation.eulerAngles.y + deg/2) % 360;
        bound.lower = (transform.rotation.eulerAngles.y - deg/2 + 360) % 360;

        base_rot = transform.rotation.eulerAngles;
        Debug.Log("upper " + bound.upper + ", lower " + bound.lower);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        Vector3 current_rot =  transform.rotation.eulerAngles;
        // current_rot.Set(current_rot.x % 360, current_rot.y % 360, current_rot.z % 360);

        // Debug.Log("rot" + current_rot.y );
        if(Math.Abs(current_rot.y - bound.upper) <= 0.001f || Math.Abs(current_rot.y - bound.lower) <= 0.001f)
        {
            // Debug.Log("eee");

            deg *= -1;

            // Vector3 new_rot = current_rot + new Vector3(0, deg, 0);
            // Debug.Log("current rot " + current_rot + "deg " + deg + "newRot: " + new_rot);
            // target_quat = Quaternion.Euler(new_rot);
            target_quat = Quaternion.Euler(new Vector3(base_rot.x, base_rot.y + deg/2, base_rot.z));
            Debug.Log(target_quat.eulerAngles);
        }

        // Debug.Log("target quat " + target_quat.eulerAngles);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target_quat, step);
    }
}
