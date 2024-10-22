using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skill2_Level", menuName = "ScriptableObjects/Skill_2DataLevel", order = 1)]
[System.Serializable]
public class Skill_2DataLevel : ScriptableObject
{
    public string SkillName;
    public int MaxHealth;
    public int PlusHealth;
}
