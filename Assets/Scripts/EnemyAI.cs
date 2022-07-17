using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public LayerMask groundMask, playerMask;
    public Vector3 walkpoint;
    public ParticleSystem muzzleFlash;
    public bool playerInSightRange, playerInAttackRange;
    public float sightRange, attackRange;
    public float walkRange;
    public float damage;
    public Transform[] waypoints;
    public Transform player;
    public Transform eye;

    public float attackSpeed;
    bool hasAtttacked;
    bool walkPointSet;
    int waypointIndex;
    Vector3 target;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //Patroling();
    }
    private void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if(player == null)
        {
            FindPlayer();
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInSightRange && !playerInAttackRange)
        {
            if (Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                Patroling();
            }
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
    private void Patroling()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!hasAtttacked)
        {
            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(eye.transform.position, eye.transform.forward, out hit, attackRange + 5f))
            {
                Target target = hit.transform.GetComponent<Target>();
                Target myCharacter = this.transform.GetComponentInParent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
            hasAtttacked = true;
            Invoke(nameof(ResetAttack), attackSpeed);
        }
    }
    private void ResetAttack()
    {
        hasAtttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(eye.transform.position, eye.forward * attackRange);
    }
}
