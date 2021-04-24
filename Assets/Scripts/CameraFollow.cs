using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject followGameObject;
    [SerializeField] Vector3 cameraOffset;

    void LateUpdate()
    {
        transform.position = followGameObject.transform.position + cameraOffset;
    }
}
