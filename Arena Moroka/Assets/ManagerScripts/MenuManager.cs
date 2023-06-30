using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnButtonLoadClick()
    {

        GameManager gameManager = GameManager.Instance;
        gameManager.health = PlayerPrefs.GetFloat("health");
        gameManager.hammer = PlayerPrefs.GetInt("hammer");
        gameManager.pistol = PlayerPrefs.GetInt("pistol");
        gameManager.auto = PlayerPrefs.GetInt("auto");
        gameManager.ammo = PlayerPrefs.GetInt("ammo");
        gameManager.roubles = PlayerPrefs.GetInt("roubles");
        gameManager.hammerUp = PlayerPrefs.GetInt("hammerUp");
        gameManager.hpUp = PlayerPrefs.GetInt("hpUp");
        gameManager.pistolUp = PlayerPrefs.GetInt("pistolUp");
        gameManager.autoUp = PlayerPrefs.GetInt("autoUp");
        gameManager.ammoUp = PlayerPrefs.GetInt("ammoUp");
        gameManager.hammerUpMax = PlayerPrefs.GetInt("hammerUpMax");
        gameManager.hpUpMax = PlayerPrefs.GetInt("hpUpMax");
        gameManager.pistolUpMax = PlayerPrefs.GetInt("pistolUpMax");
        gameManager.autoUpMax = PlayerPrefs.GetInt("autoUpMax");
        gameManager.ammoUpMax = PlayerPrefs.GetInt("ammoUpMax");
        gameManager.countW = PlayerPrefs.GetInt("countW");
        // загружаем все нужные данные игры

        SceneManager.LoadScene("SampleScene");

    }
}