using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : Colliders
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        DrawColliderBoundary();
    }
}
