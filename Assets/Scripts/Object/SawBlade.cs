
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    public float damage = 5f; 
    public float rotationSpeed = 100f; 
    void Update()
    {
        transform.Rotate(0, 0,rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemi enemy = collision.gameObject.GetComponent<Enemi>();
        if (enemy != null)
        {
            Debug.Log($"Sát thương: {damage}");
            enemy.TakeDamage(damage);
        }
    }
}
