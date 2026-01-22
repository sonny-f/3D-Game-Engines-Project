using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [Tooltip("Prefab to spawn")]
    public GameObject enemyPrefab;

    [Tooltip("How many replacements to spawn after the initial one dies")]
    public int maxReplacements = 8;

    // number of replacements already spawned
    private int replacementsSpawned = 0;

    // current active enemy instance under this controller
    private GameObject currentEnemy;

    [Tooltip("If true, spawn the first enemy at Start")]
    public bool spawnInitialAtStart = true;

    public Collider portal;

    private void Start()
    {
        if (spawnInitialAtStart)
            SpawnNew();

        portal = FindAnyObjectByType<Collider>();
    }

    private void Update()
    {
        if(replacementsSpawned == 8)
        {
            portal.isTrigger = true;
        }
        else
        {
            portal.isTrigger = false;
        }
    }

    //call if enemy already exists (e.g placed in scene)
    public void RegisterExistingEnemy(GameObject enemy)
    {
        currentEnemy = enemy;
        NewEnemy(enemy);
    }

    // called by EnemyAI when it dies (or called by this controller when spawning)
    public void OnEnemyDeath()
    {
        currentEnemy = null;

        if (replacementsSpawned >= maxReplacements)
        {
            return;
        }

        // spawn the next replacement immediately
        SpawnNew();
    }

    //spawns a new enemy and registers it
    private void SpawnNew()
    {
        GameObject spawned = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        if (spawned != null)
        {
            replacementsSpawned++;
            currentEnemy = spawned;
            NewEnemy(spawned);
        }
    }

    // ensure the spawned enemy knows about this controller so it can call back on death
    private void NewEnemy(GameObject enemy)
    {
        if (enemy == null) return;
        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        ai.spawnController = this;
    }
}
