
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void OnOpen()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClose()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClickPlay()
    {
        this.OnClose();
        GameController.instance.LoadLevel(GameController.instance.CurrentID);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has quit ");
    }
}
