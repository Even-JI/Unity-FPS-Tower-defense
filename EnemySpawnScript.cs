using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject zombie;
    public GameObject titan;
    public GameObject spooder;
    public GameObject[] bossSpawnPoints;
    public GameObject[] zombieSpawnPoints;

    private bool spawningRdy = false;
    private float spawningRate;
    private float maxSpawnDelay = 1f;

    private int kills;
    private int killsLastBossSpawn;


    private void Start()
    {
        spawningRate = 3f;
    }

    void Update()
    {
        kills = gameManager.GetComponent<GameScoreScript>().count;

        StartCoroutine(SpawnZombie(spawningRate));

        if (kills != 0 && kills % 10 == 0 && kills % 20 != 0 && kills != killsLastBossSpawn)
        {
            SpawnBoss(titan);
        }
        else if (kills != 0 && kills % 20 == 0 && kills != killsLastBossSpawn)
        {
            SpawnBoss(spooder);
        }
    }

    private IEnumerator SpawnZombie(float spawnRate)
    {
        if (spawningRdy == true) { yield break; }

        spawningRdy = true;

        yield return new WaitForSeconds(spawnRate);

        GameObject spawnPoint = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)];
        Instantiate(zombie, spawnPoint.transform.position, Quaternion.identity);
        //Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint.transform.position, Quaternion.identity);

        if (spawningRate > maxSpawnDelay)
        {
            spawningRate -= 0.01f;
        }

        Debug.Log("Delay between spawn: " + spawningRate);
        spawningRdy = false;
    }

    void SpawnBoss(GameObject boss)
    {
        killsLastBossSpawn = kills;
        GameObject bossSpawnPoint = bossSpawnPoints[Random.Range(0, bossSpawnPoints.Length)];
        Instantiate(boss, bossSpawnPoint.transform.position, Quaternion.identity);
    }
}
