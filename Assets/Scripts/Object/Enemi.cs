
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemi : MonoBehaviour
{
    private Transform player;  
    
    private float maxHealth; 
    private float damage;
    private float _currentHealthE;
   // public NavMeshAgent enemi;
    public EnemyData enemyData;
    

    void Start()
    {
        // Khởi tạo máu và sát thương từ EnemyData
        if (enemyData != null)
        {
            maxHealth = enemyData.health;
            damage = enemyData.damage;

            _currentHealthE = maxHealth;
        }
        else
        {
            Debug.LogError("EnemyData not assigned!");
        }
        //ìm kiếm player 
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // Phương thức để nhận sát thương
    public void TakeDamage(float damageTaken)
    {
    
        _currentHealthE -= damageTaken; // Trừ máu
        Debug.Log(_currentHealthE);
        if (_currentHealthE <= 0)
        {
            Die();
        }
    }


    void Update()
    {
        
        
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            transform.position += direction * enemyData.speed * Time.deltaTime;
        }

    }
    void Die()
    {
        
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(gameObject); 
        }
       
        PlayerRotation player = FindObjectOfType<PlayerRotation>();
        if (player != null)
        {
            player.OnEnemyKilled(enemyData);
        }
        Destroy(gameObject); 
    }

    public delegate void EnemyDeathHandler(GameObject enemy);
    public event EnemyDeathHandler OnEnemyDeath;

}



