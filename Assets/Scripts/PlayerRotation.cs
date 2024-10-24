

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform bulletSpawnPoint; 
    public GameObject bulletPrefab; 
    public float bulletSpeed = 10f; 
    public float shootingRadius = 15f; 
    public float shootingInterval = 1f; 

    private Transform _closestEnemy; 
    private float _lastShotTime = 0f; 
    private PlayerLevelManager _levelManager; 
    public GameObject orbitingPrefab; 
    public List<GameObject> orbitingObjects = new List<GameObject>(); 
    public float orbitRadius = 2f; 
    public float orbitSpeed = 50f; 

    public int currentSkillLevel = 0; 

    void Start()
    {
        _levelManager = GetComponent<PlayerLevelManager>(); 
    }

    void Update()
    {
        _closestEnemy = Find_closestEnemy();

        if (_closestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, _closestEnemy.position);

            if (distanceToEnemy <= shootingRadius)
            {
                Vector3 direction = (_closestEnemy.position - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

                if (Time.time - _lastShotTime > shootingInterval) 
                {
                    Shoot();
                    _lastShotTime = Time.time; 
                }
            }
        }

        UpdateOrbitingObjects();
    }

    Transform Find_closestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemi"); 
        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance) 
            {
                minDistance = distance;
                nearestEnemy = enemy.transform; 
            }
        }

        return nearestEnemy;
    }

    public void Shoot()
    {
        
        if (UiController.instance == null)
        {
            Debug.LogError("UiController.instance is null!");
            return;
        }

        if (UiController.instance.PauseLevel == null)
        {
            Debug.LogError("PauseLevel is null!");
            return;
        }

        if (currentSkillLevel < 0 || currentSkillLevel >= UiController.instance.PauseLevel.skill3Levels.Length)
        {
            Debug.LogError("Invalid skill level!");
            return;
        }

        
        int quantityBullet = UiController.instance.PauseLevel.skill3Levels[currentSkillLevel].quantityBullet;

        if (_closestEnemy == null)
        {
            Debug.LogError("No enemy found to shoot at!");
            return;
        }

        float spacing = 1f; 
        Vector3 direction = (_closestEnemy.position - bulletSpawnPoint.position).normalized; 

        for (int i = 0; i < quantityBullet; i++) 
        {
            Vector3 spawnPosition = bulletSpawnPoint.position + direction * (i * spacing);
            var bullet = Instantiate(bulletPrefab, spawnPosition, bulletSpawnPoint.rotation); 
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed; 
        }
    }

    public void OnEnemyKilled(EnemyData enemyData)
    {
        _levelManager.GainExperience(enemyData.exp);
    }

    public void TrySpawnOrbitingPrefab()
    {
        if (orbitingObjects.Count < 5) 
        {
            SpawnOrbitingPrefab(); 
        }
    }

    public void SpawnOrbitingPrefab()
    {
        Quaternion spawnRotation = Quaternion.Euler(90, 0, 0); 
        GameObject newOrbitingObject = Instantiate(orbitingPrefab, transform.position, spawnRotation, this.transform); 
        orbitingObjects.Add(newOrbitingObject);
    }

    private void UpdateOrbitingObjects()
    {
        for (int i = 0; i < orbitingObjects.Count; i++) 
        {
            if (orbitingObjects[i] != null)
            {
                float angle = Time.time * orbitSpeed + i * (360f / orbitingObjects.Count);
                float x = transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * orbitRadius;
                float z = transform.position.z + Mathf.Sin(angle * Mathf.Deg2Rad) * orbitRadius;
                orbitingObjects[i].transform.position = new Vector3(x, 0.5f, z);
            }
        }
    }
}
