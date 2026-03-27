 using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.Physics;

public class HostileAI : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePreFab;

    [Header("Layers")]
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolPoint;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isOnAttackCooldown;
    [SerializeField] private float forwardShotForce = 10;
    [SerializeField] private float verticalShotForce = 5;


    [Header("Detection Ranges")]
    [SerializeField] private float visionRange = 20f;
    [SerializeField] private float engagementRange = 10f;

    private bool isPlayerVisible;
    private bool isPlayerInRange;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void DetectPlayer()
    {
        isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }


    private void FireProjectile()
    {
        if (projectilePreFab == null || firePoint == null) return;

        Rigidbody projectileRb = Instantiate(projectilePreFab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
        projectileRb.AddForce(transform.up * verticalShotForce, ForceMode.Impulse);

        Destroy(projectileRb.gameObject, 3f);
    }

    private void FindPatorlPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);

        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //if(Physics.Raycast(potentialPoint, -transform.up, 2f, groundLayer))
        if (Physics.Raycast(potentialPoint, -transform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolPoint = true;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    private void PerformPatrol()
    {
        if (!hasPatrolPoint)
        {
            FindPatorlPoint();
        }

        if (hasPatrolPoint)
        {
            navMeshAgent.SetDestination(currentPatrolPoint);
        }
        if(Vector3.Distance(transform.position, currentPatrolPoint) < 1f)
        {
            hasPatrolPoint = false;
        }

    }

    private void PerformChase()
    {
        if(playerTransform != null)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    private void PerformAttack()
    {
        navMeshAgent.SetDestination(transform.position);

        if(playerTransform != null)
        {
            transform.LookAt(playerTransform.transform);
        }
        if (!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void UpdateBehaviourState()
    {
        if(!isPlayerVisible && !isPlayerInRange)
        {
            PerformPatrol();
        }
        else if(isPlayerVisible && !isPlayerInRange)
        {
            PerformChase();
        }
        else if (isPlayerVisible && isPlayerInRange)
        {
            PerformAttack();
        }
    }

    private void Awake()
    {
        if(playerTransform == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if(playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        if(navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        UpdateBehaviourState();
        
    }
}
