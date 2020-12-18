using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    Animator animationController;
    Rigidbody rb;
    bool isLooking;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animationController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        isLooking = true;
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius && isLooking)
        {
            agent.SetDestination(target.position);
            animationController.SetBool("Forward", true);
            if (distance < agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
        else
        {
            animationController.SetBool("Forward", false);

        }
    }
    public void ragdollActivated()
    {
        isLooking = false;
        //agent.velocity = Vector3.zero;
        //agent.isStopped = true;
        rb.isKinematic = true;
        animationController.SetBool("Forward", false);
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
