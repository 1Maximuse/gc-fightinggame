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
        if (!other.TryGetComponent(out Body body))
        {
            return;
        }
        if (other.GetComponentInParent<PlayerScript>() != null && player != other.GetComponentInParent<PlayerScript>())
        {
            if (Time.time - lastTrigger < 1.0f) return;
            if (gameObject.name.Contains("Leg"))
            {
                player.Hit("leg");
                other.GetComponentInParent<PlayerScript>().Damaged("leg");
            }
            // else if (gameObject.name.Contains("Arm") && !other.gameObject.name.Contains("Arm") && !other.gameObject.name.Contains("Leg"))
            else
            {
                player.Hit("arm");
                other.GetComponentInParent<PlayerScript>().Damaged("arm");
            }
            lastTrigger = Time.time;
        }
    }
}
