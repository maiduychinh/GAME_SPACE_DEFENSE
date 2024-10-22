
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100f; // Sát thương của đạn
    public float maxDistance = 13f; // Khoảng cách tối đa 

    private Vector3 spawnPosition; 

    void Awake()
    {
        spawnPosition = transform.position; // Lưu vị trí spawn của viên đạn
    }

    void Update()
    {
        // Kiểm tra khoảng cách từ vị trí spawn đến vị trí hiện tại của đạn
        if (Vector3.Distance(spawnPosition, transform.position) > maxDistance)
        { 
            Destroy(gameObject); 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra nếu đối tượng bị va chạm là enemy
        Enemi enemy = collision.gameObject.GetComponent<Enemi>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        
    }
}



