using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;

public class EnemyAI : MonoBehaviour
{
    [Header("General")]
    public bool fireGolem;
    public bool waterGolem;
    public bool lightningGolem;
    public bool psychicGolem;
    private bool isDying = false;
    //references
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public GameObject pinkLight;
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject blueLight;
    [Space(10)]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health = 100f;
    public Animator animator;

    public PlayerHealth playerHealth;
    [HideInInspector] public SpawnController spawnController;

    [Header("Patroling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float stoppingTime;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public EnemySpells projectile;
    public Transform castPoint;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = true;
        agent.updateRotation = true;

        fireGolem = waterGolem = lightningGolem = psychicGolem = false;

        int random = Random.Range(0, 4);

        switch(random)
        {
            case 0: fireGolem = true; redLight.SetActive(true);
                break;
            case 1:
                waterGolem = true; blueLight.SetActive(true);
                break;
            case 2:
                lightningGolem = true; yellowLight.SetActive(true);
                break;
            case 3:
                psychicGolem = true; pinkLight.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }

        if (health <= 0 && !isDying)
        {
            //set dying so it doesn't repeat
            isDying = true;
            animator.SetBool("isDead", true);
            Invoke(nameof(DestroyEnemy), 3f);
        }
    }

    private void Patroling()
    {
        animator.SetBool("isWalking", true);

        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }



        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //Check if walkPoint is walkable
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("isWalking", true);
    }

    private void AttackPlayer()
    {
        animator.SetBool("isWalking", false);

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //fire boulder
            Instantiate(projectile, castPoint.position, castPoint.rotation);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void DestroyEnemy()
    {
        // notify central spawner to create a single replacement
        spawnController.OnEnemyDeath();
        playerHealth.health = health + 20f;


        Destroy(this.gameObject);
    }
}
