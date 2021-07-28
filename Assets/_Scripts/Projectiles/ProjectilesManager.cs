using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    public static ProjectilesManager instance = null;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float spawnRate;

    [SerializeField] private float difficultyChangeRate;

    [SerializeField] private GameObject player;

    [SerializeField] private Rigidbody2D prefab;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private bool canSpawn = true;

    private int projectileCount;
    public int Projectiles { get { return projectileCount; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        //StartSpawning();
    }

    public void StartSpawning()
    {
        canSpawn = true;
        projectileCount = 0;
        StartCoroutine(SpawnRoutine());
        StartCoroutine(DifficultyIncreaser());
    }

    private void OnEnable()
    {
        Projectile.OnProjectileHit += Projectile_OnProjectileHit;
    }
    private void OnDisable()
    {
        Projectile.OnProjectileHit -= Projectile_OnProjectileHit;
    }

    private void Projectile_OnProjectileHit()
    {
        canSpawn = false;
        GameManager.instance.GameOver();
    }


    public void Spawn()
    {
        Rigidbody2D projectileInstance = Instantiate(prefab, spawnPoints[Random.Range(0, spawnPoints.Count - 1)]);
        Vector2 direction = player.transform.position - projectileInstance.transform.position;
        projectileInstance.AddRelativeForce(direction * projectileSpeed);
        projectileCount++;
    }

    //Changes difficulty according to time
    private IEnumerator DifficultyIncreaser()
    {
        while (canSpawn)
        {
            projectileSpeed++;

            if (spawnRate > 0.1f)
                spawnRate -= 0.1f;

            yield return new WaitForSeconds(difficultyChangeRate);
        }
    }

    
    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnRate);
            Spawn();           
        }
    }
}
