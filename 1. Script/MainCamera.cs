using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform playerTransform;

    public float offsetY;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        Vector3 tmp = transform.position;
        tmp.x = playerTransform.position.x;
        tmp.y = playerTransform.position.y;
        tmp.y += offsetY;
        transform.position = tmp;
    }
}
