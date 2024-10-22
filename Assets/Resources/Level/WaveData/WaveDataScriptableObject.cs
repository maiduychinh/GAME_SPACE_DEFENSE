using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]

[System.Serializable]
public class WaveDataScriptableObject : ScriptableObject
{
    public RoundData[] rounds; 
}