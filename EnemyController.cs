using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float health = 100;
    public float lookRadius = 10f;
    public GameObject gameManager;

    public bool deathFallBackwards = true;

    Animator animator;
    Transform target;
    NavMeshAgent agent;

    private bool dead;
    private GameObject NPC;




    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //target = PlayerManager.instance.player.transform;
        target = EnemyGoalManager.instance.EnemyGoal.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            NPC = other.gameObject;
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC") && dead == false)
        {
            target = other.gameObject.transform;
            other.gameObject.GetComponent<NPCController>().enemyClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NPC = null;
        target = EnemyGoalManager.instance.EnemyGoal.transform;
    }


    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);


        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("Moving", true);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

        else
        {
            animator.SetBool("Moving", false);
        }

        if (health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    void FaceTarget ()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator Death()
    {
        dead = true;
        if (NPC != null)
        {
            NPC.GetComponent<NPCController>().enemyClose = false;
        }
        GetComponent<SphereCollider>().enabled = false;
        gameManager.GetComponent<GameScoreScript>().count++;
        agent.SetDestination(gameObject.transform.position);
        animator.SetTrigger("FallBack");
        //if (deathFallBackwards == true) { animator.SetTrigger("FallBack"); }
        //else { animator.SetTrigger("FallForward"); }
        agent.enabled = false;
        gameObject.GetComponent<EnemyController>().enabled = false;

        yield return new WaitForSeconds(15f);

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
