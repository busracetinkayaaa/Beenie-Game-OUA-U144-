using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntManager : MonoBehaviour
{
    public NavMeshAgent _agent;
    public Animator _animator;
    [SerializeField] Transform _player;
    public LayerMask ground, player;

    public Vector3 destinationPoint;
    private bool destinationPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject sphere;

    public float sightRange, attackRange;
    public bool playerInsightRange, playerInAttackRange;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        if (!playerInsightRange && !playerInAttackRange)
        {
            Patroling();
            _animator.SetBool("walking", true);
            _animator.SetBool("attacking", false);
        }
        if (playerInsightRange && !playerInAttackRange)
        {
            ChasePlayer();
            _animator.SetBool("walking", true);
            _animator.SetBool("attacking", false);
        }
        if (playerInsightRange && playerInAttackRange)
        {
            AttackPlayer();
            _animator.SetBool("walking", false);
            _animator.SetBool("attacking", true);
        }
    }

    void Patroling()
    {
        if (!destinationPointSet || _agent.remainingDistance < 1.0f)
        {
            SearchWalkPoint();
        }

        if (destinationPointSet)
        {
            _agent.SetDestination(destinationPoint);
        }

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        if (distanceToDestinationPoint.magnitude < 1.0f)
        {
            destinationPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            destinationPointSet = true;
        }
    }

    void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }

    void AttackPlayer()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);
        if (!alreadyAttacked)
        {
            Rigidbody rd = Instantiate(sphere, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rd.AddForce(transform.forward * 25f, ForceMode.Impulse);
            rd.AddForce(transform.up * 7f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
