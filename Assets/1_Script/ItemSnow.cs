using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSnow : MonoBehaviour
{
    public float rotateSpeed;
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}