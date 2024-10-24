
using UnityEngine;
using UnityEngine.UI;

public class Enemi : MonoBehaviour
{
    private Transform player;
    private float maxHealth;
    private float damage;
    private float _currentHealthE;

    public EnemyData enemyData;
    public GameObject damageTextPrefab; // Prefab chứa Text UI
    public float textOffsetY = 0f; // Khoảng cách để hiển thị text

    void Start()
    {
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

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    public void TakeDamage(float damageTaken)
    {
        _currentHealthE -= damageTaken;
        ShowDamageText(damageTaken); // Hiển thị sát thương

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

    // Hiển thị số sát thương
    public void ShowDamageText(float damageTaken) // Đổi phương thức thành public
    {
        if (damageTextPrefab != null)
        {
            // Tạo Text UI từ Prefab
            GameObject damageText = Instantiate(damageTextPrefab, transform.position + new Vector3(-2, 1, 0), Quaternion.identity);

            // Lấy component Text trong Prefab
            Text textComponent = damageText.GetComponentInChildren<Text>();

            if (textComponent != null)
            {
                // Hiển thị lượng máu đã bị trừ
                textComponent.text = "-" + damageTaken.ToString(); // Thêm dấu trừ để chỉ ra lượng máu mất đi
            }
            else
            {
                Debug.LogError("Text component not found in damageTextPrefab!");
            }

            Destroy(damageText, 0.2f); // Hủy sau 0.1 giây
        }
        else
        {
            Debug.LogError("damageTextPrefab is not assigned!");
        }
    }
    

}
