using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Inicio()
    {
        SceneManager.LoadScene("INICIO");
    }

    public void Level1()
    {
        SceneManager.LoadScene("JOSETEST");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CREDITS");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
