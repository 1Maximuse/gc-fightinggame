using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    [SerializeField]
    private Transform[] target;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 average = Vector3.zero;
        foreach (Transform t in target)
        {
            average += t.position;
        }
        average /= target.Length;

        transform.position = Vector3.SmoothDamp(transform.position, average, ref velocity, 0.4f);
    }
}
