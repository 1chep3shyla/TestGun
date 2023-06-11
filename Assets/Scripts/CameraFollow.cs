using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float offY;
    public float offZ;
    public Transform target;
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + offY, target.position.z + offZ);
    }
}
