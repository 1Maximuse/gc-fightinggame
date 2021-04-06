using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Colliders : MonoBehaviour
{
    protected void DrawColliderBoundary()
    {
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (BoxCollider c in colliders)
        {
            Gizmos.matrix = c.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(c.center, c.size);
        }
    }
}
