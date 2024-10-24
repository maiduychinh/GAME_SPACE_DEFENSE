
using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxArmor = 0f;
    private float _curentHealth;

    [SerializeField] private HealthBar _healthBar;
 
    [SerializeField] private ArmorBar _armorBar; 

    void Start()
    {
      
        _curentHealth = maxHealth;
        _healthBar.UpdateHealthbarPlayer(maxHealth, _curentHealth);
        _armorBar.UpdateArmorbarPlayer(maxArmor);
    }

    void OnCollisionEnter(Collision collision)
    {
        Enemi enemy = collision.gameObject.GetComponent<Enemi>();
        if (enemy != null)
        {
           TakeDamage(enemy.enemyData.damage);
        }
    }

   
    void TakeDamage(float damage)
    {
        float damageToTake = damage - maxArmor;

        if (maxArmor > 0)
        {
            if (damageToTake < maxArmor) damageToTake = 0; 

        }

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
        maxHealth = skillData.MaxHealth;
        _curentHealth += skillData.PlusHealth;

        if (_curentHealth > maxHealth)
        {
            _curentHealth = maxHealth;
        }

        _healthBar.UpdateHealthbarPlayer(maxHealth, _curentHealth);
        Debug.Log($"Máu sau khi sử dụng Skill 2: {_curentHealth} / {maxHealth}");
    }
    
    public void ApplySkill_5(Skill_5DataLevel skillData)
    {
        maxArmor = skillData.armor;
        _armorBar.UpdateArmorbarPlayer(maxArmor);
    }

    public void CreaseHealth(Skill_2DataLevel skillData)
    {
        _curentHealth += skillData.PlusHealth; 
        maxHealth = skillData.MaxHealth; 
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
