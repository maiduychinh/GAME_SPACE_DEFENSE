
using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxArmor = 0f;
    private float _curentHealth;

    [SerializeField] private HealthBar _healthBar;
 
    [SerializeField] private ArmorBar _armorBar; // Nếu bạn có thanh giáp

    void Start()
    {
        // Khởi tạo máu hiện tại bằng giá trị máu tối đa
        _curentHealth = maxHealth;
        _healthBar.UpdateHealthbarPlayer(maxHealth, _curentHealth);
        _armorBar.UpdateArmorbarPlayer(maxArmor);
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemi enemy = collision.gameObject.GetComponent<Enemi>();
        if (enemy != null)
        {
            // Trừ máu ngay lập tức khi va chạm với enemy
           TakeDamage(enemy.enemyData.damage);
        }
    }

   
    void TakeDamage(float damage)
    {
        // Tính toán lượng sát thương sau khi trừ giáp
        float damageToTake = damage - maxArmor;

        // Nếu giáp đủ để hấp thụ sát thương
        if (maxArmor > 0)
        {
            if (damageToTake < maxArmor) damageToTake = 0; // Không trừ máu nếu giáp hấp thụ toàn bộ sát thương

        }

        // Trừ máu
        _curentHealth -= damageToTake;
        Debug.Log($"Player took {damageToTake} damage, remaining health: {_curentHealth}");
        _healthBar.UpdateHealthbarPlayer(maxHealth, _curentHealth);
        if (_curentHealth <= 0)
        {
            Die();
        }
    }
    public void ApplySkill_2(Skill_2DataLevel skillData)
    {
        // Cập nhật maxHealth và tăng máu hiện tại dựa trên dữ liệu từ Skill_2DataLevel
        maxHealth = skillData.MaxHealth;
        _curentHealth += skillData.PlusHealth;

        // Đảm bảo rằng _curentHealth không vượt quá maxHealth
        if (_curentHealth > maxHealth)
        {
            _curentHealth = maxHealth;
        }

        // Cập nhật thanh máu trên UI
        _healthBar.UpdateHealthbarPlayer(maxHealth, _curentHealth);
        Debug.Log($"Máu sau khi sử dụng Skill 2: {_curentHealth} / {maxHealth}");
    }
    
    public void ApplySkill_5(Skill_5DataLevel skillData)
    {
        maxArmor = skillData.armor;
        _armorBar.UpdateArmorbarPlayer(maxArmor);
    }

    // Cập nhật lại hàm để nhận tham số Skill_2DataLevel
    public void CreaseHealth(Skill_2DataLevel skillData)
    {
        _curentHealth += skillData.PlusHealth; // Cộng thêm PlusHealth từ Skill_2DataLevel
        maxHealth = skillData.MaxHealth; // Gán MaxHealth từ Skill_2DataLevel
        _healthBar.UpdateHealthbarPlayer(maxHealth, _curentHealth);
        Debug.Log($"máu còn {_curentHealth} / {maxHealth}");
    }

    void Die()
    {
        Debug.Log("Player has died!");
        Destroy(gameObject);
        GameController.instance.DoOver();
    }
}
