using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager_Script : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
