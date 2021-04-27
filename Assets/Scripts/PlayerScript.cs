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

    private MlAgent mlAgent;

    private Animator animator;
    private Quaternion initialRotation;

    private int hp;

    [SerializeField]
    private int kickDamage;

    [SerializeField]
    private int punchDamage;

    [SerializeField]
    private GameController gameController;
    public void Hit(string trigger)
    {
        Debug.Log("hit " + gameObject.name);

        if (trigger == "leg") mlAgent.hit(kickDamage);
        else mlAgent.hit(punchDamage);
    }

    public int GetHP()
    {
        return hp;
    }

    public void Damaged(string trigger)
    {
        Debug.Log("Damaged " + gameObject.name);
        int damage;

        if (trigger == "leg") damage = kickDamage;
        else damage = punchDamage;

        hp -= damage;
        if (hp > 0)
        {
            mlAgent.damaged(damage);
        } else
        {
            mlAgent.died();
            otherPlayer.GetComponent<MlAgent>().EndEpisode();
            reset();
            otherPlayer.GetComponent<PlayerScript>().reset();
            if (gameObject.name == "Player")
            {
                gameController.Lose();
            } else
            {
                gameController.Win();
            }
        }
    }
    public void reset()
    {
        hp = 100;
    }

    void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        initialRotation = transform.rotation;
        mlAgent = GetComponent<MlAgent>();
        hp = 100;
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
        if (transform.position.z > otherPlayer.position.z && mlAgent != null)
        {
            if (!mlAgent.kebalik)
            {
                Debug.Log("masuk");
                mlAgent.kebalik = true;
                initialRotation *= Quaternion.Euler(0, 180f, 0);
            }
        } 
        else if (mlAgent != null)
        {
            if (mlAgent.kebalik)
            {
                mlAgent.kebalik = false;
                // transform.Rotate(new Vector3(0, 180f, 0));
                initialRotation *= Quaternion.Euler(0, 180f, 0);
            }
        }
    }
}
