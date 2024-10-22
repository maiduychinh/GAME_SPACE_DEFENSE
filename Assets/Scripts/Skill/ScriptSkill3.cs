using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSkill3 : MonoBehaviour
{
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
        Debug.Log("Skill3 Open");
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Skill3 close");
    }
}
