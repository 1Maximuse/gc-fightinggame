using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Colliders
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        DrawColliderBoundary();
    }
}
