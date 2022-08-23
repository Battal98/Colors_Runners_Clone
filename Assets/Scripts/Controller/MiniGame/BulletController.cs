using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float BulletMovementSpeed;
    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * BulletMovementSpeed));
    }
}
