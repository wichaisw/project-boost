using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float zAngle = 1f;

    void Update()
    {
        GetComponent<Transform>().Rotate(0, 0, zAngle, Space.Self);
    }
}
