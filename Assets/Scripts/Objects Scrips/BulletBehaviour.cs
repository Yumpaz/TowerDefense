using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    private int force, rot;

    void Start()
    {
        
    }

    public void Movement(Vector3 targetPosition, Vector3 originPosition)
    {
        Vector3 moveDir = (targetPosition - originPosition).normalized;
        transform.position = transform.position + moveDir * 15 * Time.deltaTime;
    }
}
