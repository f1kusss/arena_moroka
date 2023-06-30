using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameManager gameManager = GameManager.Instance;
        gameManager.health = 100;
        gameManager.hammer = 25;
        gameManager.pistol = 15;
        gameManager.auto = 10;
        gameManager.ammo = 30;

        gameManager.hammerUp = 1;
        gameManager.hpUp = 1;
        gameManager.pistolUp = 1;
        gameManager.autoUp = 1;
        gameManager.ammoUp = 1;

        gameManager.hammerUpMax = 0;
        gameManager.hpUpMax = 0;
        gameManager.pistolUpMax = 0;
        gameManager.autoUpMax = 0;
        gameManager.ammoUpMax = 0;

        gameManager.countW = 0;

        gameManager.nearTorgash = false;

        gameManager.inFight = false;
        SceneManager.LoadScene("MenuScene");
    }
    public void SaveGame()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.SaveData();
    }

}
