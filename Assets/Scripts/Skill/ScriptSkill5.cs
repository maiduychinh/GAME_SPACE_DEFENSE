using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSkill5 : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Skill5 Open");
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Skill5 close");
    }
}
