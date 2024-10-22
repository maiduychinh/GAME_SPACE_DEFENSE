﻿


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public WaveDataScriptableObject[] way; 
    public float timeBetweenSpawns = 2f; // Thời gian spawn
    public float timeBetweenRounds = 5f; // Time delay round
    private int currentWay; 
    private int instanceNumber = 1; 
    private int enemiesRemaining = 0; // Số lượng enemy còn lại trong round hiện tại

    public Transform TransformSpawn;

    public PlayerRotation PlayerRotation;

    public DamageZone damageZone;
    void Start()
    {
       
        StartCoroutine(SpawnWay());
    }

    IEnumerator SpawnWay()
    {
        while (currentWay < way.Length) // Vòng lặp cho đến khi hết các cấp độ
        {
            WaveDataScriptableObject currentLevelData = way[currentWay]; // Lấy dữ liệu của cấp độ hiện tại

            // Vòng lặp qua các round trong level hiện tại
            for (int roundIndex = 0; roundIndex < currentLevelData.rounds.Length; roundIndex++)
            {
                RoundData currentRound = currentLevelData.rounds[roundIndex]; // Lấy dữ liệu của round hiện tại
                enemiesRemaining = 0; // Reset lại số lượng enemy cho round


                // Spawn các loại enemy trong round
                for (int enemyTypeIndex = 0; enemyTypeIndex < currentRound.enemyTypes.Length; enemyTypeIndex++)
                {
                    EnemyData enemyType = currentRound.enemyTypes[enemyTypeIndex];
                    int count = currentRound.enemyCounts[enemyTypeIndex];
                    enemiesRemaining += count; // Cập nhật số lượng enemy còn lại trong round

                    for (int i = 0; i < count; i++)
                    {
                        SpawnEntity(enemyType);
                        yield return new WaitForSeconds(timeBetweenSpawns); // Chờ giữa các lần spawn
                    }
                }

                // Chờ cho đến khi tất cả enemy trong round bị tiêu diệt
                yield return new WaitUntil(() => enemiesRemaining <= 0);

                // Chờ trước khi bắt đầu round tiếp theo
                yield return new WaitForSeconds(timeBetweenRounds);
            }

            currentWay++; // Chuyển sang cấp độ tiếp theo
        }

        GameController.instance.DoWin();

    }

    void SpawnEntity(EnemyData enemyData)
    {
        if (enemyData == null)
        {
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject currentEntity = Instantiate(enemyData.enemyPrefab, spawnPosition, Quaternion.identity , TransformSpawn);
        currentEntity.name = enemyData.enemyName + instanceNumber;
        instanceNumber++;

        // Gán dữ liệu EnemyData vào script Enemi
        Enemi enemyScript = currentEntity.GetComponent<Enemi>();
        if (enemyScript != null)
        {
            enemyScript.enemyData = enemyData; // Gán dữ liệu từ EnemyData
            enemyScript.OnEnemyDeath += HandleEnemyDeath; // Đăng ký với sự kiện khi enemy chết
        }
        else
        {
        }
    }

    // Xử lý khi enemy chết
    void HandleEnemyDeath(GameObject enemy)
    {
        enemiesRemaining--; 
    }

    Vector3 GetRandomSpawnPosition()
    {
      
        float x, z;

        if (Random.value < 0.5f)
        {
            x = Random.Range(-25, -15); 
        }
        else
        {
            x = Random.Range(15, 25); 
        }

        if (Random.value < 0.5f)
        {
            z = Random.Range(-25, -15); 
        }
        else
        {
            z = Random.Range(15, 25); 
        }

        return new Vector3(x, 1, z);
    }
}