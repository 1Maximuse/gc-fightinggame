using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private bool controlled;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private Transform otherPlayer;

    private Animator animator;
    private Quaternion initialRotation;

    public void Hit()
    {
        Debug.Log("hit " + gameObject.name);
    }

    public void Damaged()
    {
        Debug.Log("Damaged " + gameObject.name);
    }

    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        initialRotation = transform.rotation;
    }

    void Update()
    {
        transform.position = new Vector3(0f, 0f, transform.position.z);
        transform.rotation = initialRotation;
        if (controlled)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetBool("Forward", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animator.SetBool("Forward", false);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("Backward", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animator.SetBool("Backward", false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Jab");
            }

            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Kick");
            }
        }
        if (Mathf.Abs(transform.position.z - otherPlayer.position.z) < minDistance)
        {
            bool direction = (transform.position.z - otherPlayer.position.z) > 0;
            transform.position = transform.position - new Vector3(0f, 0f, 10f * Time.deltaTime * (direction ? -1 : 1));
        }
    }
}
