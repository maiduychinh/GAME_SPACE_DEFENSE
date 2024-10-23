
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damagePerSecond = 20f; // Số damage mỗi giây
    public float radius = 5f; // Bán kính của vùng damage
    private List<Enemi> enemiesInZone = new List<Enemi>();

    private void Start()
    {
        UpdateScale();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemi enemy = other.GetComponent<Enemi>();
        if (enemy != null)
        {
            enemiesInZone.Add(enemy);
            StartCoroutine(DamageEnemyOverTime(enemy));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemi enemy = other.GetComponent<Enemi>();
        if (enemy != null)
        {
            enemiesInZone.Remove(enemy);
        }
    }

    private IEnumerator DamageEnemyOverTime(Enemi enemy)
    {
        while (enemiesInZone.Contains(enemy))
        {
            if (enemy == null)
            {
                enemiesInZone.Remove(enemy);
                yield break;
            }

            enemy.TakeDamage(damagePerSecond); // Trừ máu
            yield return new WaitForSeconds(1f); // Chờ 1 giây trước khi trừ máu tiếp
        }
    }

    
    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        UpdateScale();
    }

    private void UpdateScale()
    {
        float a = 0.01f;
        transform.localScale = new Vector3(radius, a, radius);
    }
}
