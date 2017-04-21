using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    public float staggerTime;
    public float health;
    public float decomposeTime;
    public float attackInterval;

    private GameObject target;
    private NavMeshAgent agent;
    private Animator anim;
    private Vector3 targetLoc;
    private bool attacking;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetLoc = target.transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetLoc);
        agent.stoppingDistance = 1.5f;

        anim = GetComponent<Animator>();

        attacking = false;

    }

	// Update is called once per frame
	void Update () {

        Vector3 direction = (target.transform.position = transform.position).normalized;
        Quaternion look

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            lookRotation, Time.deltaTime * rotationSpeed);

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Halt(staggerTime));
            health -= 5;
        }

        if (health <= 0)
        {
            StartCoroutine(Die(decomposeTime));
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            attacking = true;
            StartCoroutine(Attack(attackInterval));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            attacking = false;
        }
    }

    IEnumerator Halt(float time)
    {
        agent.isStopped = true;
        anim.Play("wound", 0);
        yield return new WaitForSecondsRealtime(time);
        anim.SetBool("idle0ToRun", true);
        agent.isStopped = false;
    }

    IEnumerator Die(float dietime)
    {
        agent.isStopped = true;
        anim.Play("death", 0);
        yield return new WaitForSecondsRealtime(dietime);
        Destroy(gameObject);
    }
    IEnumerator Attack(float attackintev)
    {
        while (attacking == true)
        {
            anim.SetBool("idle0ToRun", false);
            anim.SetBool("runToIdle0", true);
            anim.SetBool("idle0ToAttack1", true);
            print("Damage");
            yield return new WaitForSecondsRealtime(attackintev);
            //anim.SetBool("runToIdle0", false);
        }
    }
}
