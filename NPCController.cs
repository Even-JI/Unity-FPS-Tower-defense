using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float health = 100;

    public GameObject gameManager;

    Animator animator;
    Transform target;
    NavMeshAgent agent;

    public bool enemyClose;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        target = EnemyGoalManager.instance.EnemyGoal.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.SetDestination(target.position);
    }

    void Update()
    {
        if (enemyClose == true)
        {
            Sprint();
        }
        else
        {
            Run();
        }

        FaceTarget();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Run()
    {
        agent.speed = 1;
        animator.SetBool("Run", true);
        animator.SetBool("Sprint", false);
    }

    void Sprint()
    {
        agent.speed = 2;
        animator.SetBool("Sprint", true);
        animator.SetBool("Run", false);
    }
}
