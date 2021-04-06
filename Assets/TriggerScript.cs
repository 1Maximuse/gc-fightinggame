using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    private PlayerScript player;
    private float lastTrigger = 0;

    private void OnEnable()
    {
        player = GetComponentInParent<PlayerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerScript>() != null && player != other.GetComponentInParent<PlayerScript>())
        {
            if (Time.time - lastTrigger < 1.0f) return;
            player.Hit();
            other.GetComponentInParent<PlayerScript>().Damaged();
            lastTrigger = Time.time;
        }
    }
}
