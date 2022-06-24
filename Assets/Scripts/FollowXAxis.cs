using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowXAxis : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float shift = 0f;

    void Update()
    {
        Vector3 position = transform.position;
        position.x = target.position.x + shift;
        transform.position = position;
    }
}
