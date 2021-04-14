using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MlAgent : Agent
{
    private Animator animator;
    private Academy academy;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform leftArmTransform;
    [SerializeField] private Transform rightArmTransform;
    [SerializeField] private Transform leftFootTransform;
    [SerializeField] private Transform rightFootTransform;

    [SerializeField] private Vector3 initialPosition;


    protected override void OnEnable()
    {
        // academy = Academy.Instance;
        // academy.AutomaticSteppingEnabled = false;
        Time.captureDeltaTime = 0;
        animator = GetComponentInChildren<Animator>();
        Debug.Log("OnEnable ngab");
    }

    public override void OnEpisodeBegin()
    {
        // InvokeRepeating("waiterStep", 0f, 1f);
        Debug.Log("Mulai ngab");
        transform.position = initialPosition;
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log("Action");
        int move = actions.DiscreteActions[0];

        if (move == 0)
        {
            StartCoroutine(waiterForward());
        }
        else if (move == 1)
        {
            StartCoroutine(waiterBackward());
        }
        else if (move == 2)
        {
            animator.SetTrigger("Jab");
        }
        else
        {
            animator.SetTrigger("Kick");
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log("Observe ngab");
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(bodyTransform.localPosition);
        sensor.AddObservation(leftArmTransform.localPosition);
        sensor.AddObservation(rightArmTransform.localPosition);
        sensor.AddObservation(leftFootTransform.localPosition);
        sensor.AddObservation(rightFootTransform.localPosition);
    }

    public void hit(int trigger)
    {
        AddReward(trigger);
    }

    public void damaged(int trigger)
    {
        AddReward(-trigger);
    }

    public void died()
    {
        EndEpisode();
    }
    IEnumerator waiterForward()
    {
        animator.SetBool("Forward", true);
        yield return new WaitForSeconds(0.9f);
        animator.SetBool("Forward", false);
    }

    IEnumerator waiterBackward()
    {
        animator.SetBool("Backward", true);
        yield return new WaitForSeconds(0.9f);
        animator.SetBool("Backward", false);
    }
    private void waiterStep()
    {
        Debug.Log("step");
        academy.EnvironmentStep();
    }


}
