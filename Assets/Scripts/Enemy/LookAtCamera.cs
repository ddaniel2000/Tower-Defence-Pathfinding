using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        RotateToCamera();
    }

    private void RotateToCamera()
    {
        gameObject.transform.LookAt(_camera.transform.position);
    }
}
