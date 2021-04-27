using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject lose;

    [SerializeField]
    private GameObject win;

    public void Win()
    {
        Time.timeScale = 0;
        win.SetActive(true);
    }
    public void Lose()
    {
        Time.timeScale = 0;
        lose.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
