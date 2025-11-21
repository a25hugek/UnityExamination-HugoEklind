using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -10f);
    [SerializeField] private float smoothing = 3.0f;

    // Update is called once per frame
    void LateUpdate() {

        Vector3 newPos = Vector3.Lerp(transform.position, target.position+offset, smoothing*Time.deltaTime);
        transform.position = newPos;

    }
}
