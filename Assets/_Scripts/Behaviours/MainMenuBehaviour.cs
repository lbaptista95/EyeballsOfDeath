using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuBehaviour : MonoBehaviour
{
    public void OpenSkinMenu()
    {
        GameManager.instance.OpenSkinMenu();
    }

    public void StartNewGame()
    {
        SceneChangeManager.instance.GoToScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
