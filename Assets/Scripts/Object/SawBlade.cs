
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    public float damage = 5f; // Sát thương của lưỡi cưa
    public float rotationSpeed = 100f; // Tốc độ xoay

    void Update()
    {
        // Xoay đối tượng quanh trục Y với tốc độ rotationSpeed
        transform.Rotate(0, 0,rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra nếu đối tượng bị va chạm là enemy
        Enemi enemy = collision.gameObject.GetComponent<Enemi>();
        if (enemy != null)
        {
            Debug.Log($"Sát thương: {damage}");
            enemy.TakeDamage(damage);
        }
    }
}
