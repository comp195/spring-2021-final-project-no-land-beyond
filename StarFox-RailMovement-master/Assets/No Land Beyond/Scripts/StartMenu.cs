using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public void change_scene()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "Play")
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}
