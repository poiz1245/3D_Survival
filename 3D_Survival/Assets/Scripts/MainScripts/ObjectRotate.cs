using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.Rotate(0f, speed, 0f);
    }
}
