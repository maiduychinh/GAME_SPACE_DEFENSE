using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
[System.Serializable]
public class EnemyData : ScriptableObject
{
    public string enemyName; 
    public float health; 
    public float damage;
    public float exp;
    public float speed;
    public GameObject enemyPrefab; 
}

