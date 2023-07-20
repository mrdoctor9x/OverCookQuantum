using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (target == null)
            return;
        //transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = target.position;
    }
    public void SetupTarget(Transform target)
    {
        Debug.Log("init camera player");
        this.target = target;
    }
}
