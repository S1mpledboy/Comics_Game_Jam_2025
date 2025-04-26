using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float cameraMaxX, cameraMaxY;


    Vector3 CalmpCameraMovement()
    {
        float newX = Mathf.Clamp(_player.position.x, -cameraMaxX, cameraMaxX);
        float newY = Mathf.Clamp(_player.position.y, -cameraMaxY, cameraMaxY);
        return new Vector3(newX, newY, transform.position.z);
    }
    private void LateUpdate()
    {
        transform.LookAt(_player);
        transform.position = CalmpCameraMovement();
        transform.rotation = Quaternion.identity;
    }
}
