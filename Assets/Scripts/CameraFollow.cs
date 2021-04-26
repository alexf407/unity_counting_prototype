using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject followGameObject;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float rotationSpeed = 1f;
    Vector3 targetPos;

    void LateUpdate()
    {
        targetPos = followGameObject.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetPos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
