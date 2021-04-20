using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField]
    private MlAgent[] agents;

    private void OnTriggerEnter(Collider other)
    {
        foreach (MlAgent agent in agents)
        {
            agent.HitBoundaries();
        }
    }
}