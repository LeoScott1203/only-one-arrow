using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float spawnDistanceFromPlayer = 10.0f;

    [SerializeField]
    float startTimeBetweenSpawns = 5.0f;
    [SerializeField]
    float minTimeBetweenSpawns = 1.0f;
    [SerializeField]
    float reduceTimeBetweenSpawnsKillFactor = 0.1f; // How much each kill reduces the time between spawns

    float timeBetweenSpawns = 0.0f;
    float timeSinceLastSpawn = 0.0f;

    [SerializeField]
    Enemy enemyPrefab;

    bool spawnEnemy = true;

    public void Awake()
    {
        timeBetweenSpawns = startTimeBetweenSpawns;
    }

    public void Start()
    {
        timeSinceLastSpawn = timeBetweenSpawns;
    }

    public void OnEnable()
    {
        Arrow.OnHit += OnEnemyKilled; // Since atm we can just assume it's enemies and all enemies have 1 hp
        EnemyMovementBehaviour.TriggerMenu += OnEnd;
        Reset.OnReset += OnReset;
    }

    public void OnDisable()
    {
        Arrow.OnHit -= OnEnemyKilled; // Since we can just assume it's enemies and all enemies have 1 hp
        EnemyMovementBehaviour.TriggerMenu -= OnEnd;
        Reset.OnReset -= OnReset;
    }

    void OnReset()
    {
        timeBetweenSpawns = startTimeBetweenSpawns;
        timeSinceLastSpawn = timeBetweenSpawns;

        spawnEnemy = true;
    }

    public void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        
        if(timeSinceLastSpawn >= timeBetweenSpawns && spawnEnemy == true)
        {
            timeSinceLastSpawn = 0.0f;
            Spawn();
        }
    }

    void OnEnemyKilled(Collider2D col, ITarget target, Arrow arrow)
    {
        timeBetweenSpawns = Mathf.Max(minTimeBetweenSpawns, timeBetweenSpawns - reduceTimeBetweenSpawnsKillFactor);
    }

    void Spawn()
    {
        Vector2 spawnPointUncentered = Random.insideUnitCircle.normalized * spawnDistanceFromPlayer;
        Vector3 spawnPointCenteredOnPlayer = new Vector3(spawnPointUncentered.x, spawnPointUncentered.y, 0) + Player.MainPlayer.transform.position;
        GameObject.Instantiate(enemyPrefab, spawnPointCenteredOnPlayer, Quaternion.identity);
    }

    void OnEnd(EnemyMovementBehaviour EMB)
    {

        spawnEnemy = false;

    }
}