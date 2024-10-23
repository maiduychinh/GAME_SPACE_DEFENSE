using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]

[System.Serializable]
public class WaveDataScriptableObject : ScriptableObject
{
    public EnemyData[] enemyTypes; // Loại enemy xuất hiện trong wave
    public int[] enemyCounts; // Số lượng enemy tương ứng
}