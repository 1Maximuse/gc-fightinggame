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

    public bool kebalik;

    protected override void OnEnable()
    {
        kebalik = false;
        base.OnEnable();
        // academy = Academy.Instance;
        // academy.AutomaticSteppingEnabled = false;
        animator = GetComponentInChildren<Animator>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = initialPosition;
        // InvokeRepeating("waiterStep", 0f, 1f); 
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        int move = actions.DiscreteActions[0];

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (move == 0)
            {
                if (kebalik) StartCoroutine(waiterBackward());
                else StartCoroutine(waiterForward());
            }
            else if (move == 1)
            {
                if (!kebalik) StartCoroutine(waiterBackward());
                else StartCoroutine(waiterForward());
            }
            else if (move == 2)
            {
                animator.SetTrigger("Jab");
            }
            else if (move == 3)
            {
                animator.SetTrigger("Kick");
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        discreteActions[0] = 4;

        if (Input.GetKeyDown(KeyCode.D))
        {
            discreteActions[0] = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            discreteActions[0] = 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            discreteActions[0] = 2;
        }

        if (Input.GetMouseButtonDown(1))
        {
            discreteActions[0] = 3;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
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

    public void HitBoundaries()
    {
        SetReward(-100);
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
        academy.EnvironmentStep();
    }


}
