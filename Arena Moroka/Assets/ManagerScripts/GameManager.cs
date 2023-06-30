using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public int roubles = 0;
    public bool notInMenuScene = false;
    public string scene;

    public static bool GameIsPaused = false;

    public float health = 100;
    public int hammer = 25;
    public int pistol = 15;
    public int auto = 10;
    public int ammo = 30;

    public int hammerUp = 1;
    public int hpUp = 1;
    public int pistolUp = 1;
    public int autoUp = 1;
    public int ammoUp = 1;

    public int hammerUpMax = 0;
    public int hpUpMax = 0;
    public int pistolUpMax = 0;
    public int autoUpMax = 0;
    public int ammoUpMax = 0;

    public int countW = 0;

    public bool nearTorgash = false;

    public bool inFight = false;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр GameManager
        if (instance == null)
        {
            // Если экземпляр GameManager не существует, делаем текущий экземпляр постоянным через все сцены
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            // Если экземпляр GameManager уже существует, уничтожаем текущий экземпляр
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("health", health);
        PlayerPrefs.SetInt("hammer", hammer);
        PlayerPrefs.SetInt("pistol", pistol);
        PlayerPrefs.SetInt("auto", auto);
        PlayerPrefs.SetInt("ammo", ammo);
        PlayerPrefs.SetInt("roubles", roubles);
        PlayerPrefs.SetInt("hammerUp", hammerUp);
        PlayerPrefs.SetInt("hpUp", hpUp);
        PlayerPrefs.SetInt("pistolUp", pistolUp);
        PlayerPrefs.SetInt("autoUp", autoUp);
        PlayerPrefs.SetInt("ammoUp", ammoUp);
        PlayerPrefs.SetInt("hammerUpMax", hammerUpMax);
        PlayerPrefs.SetInt("hpUpMax", hpUpMax);
        PlayerPrefs.SetInt("pistolUpMax", pistolUpMax);
        PlayerPrefs.SetInt("autoUpMax", autoUpMax);
        PlayerPrefs.SetInt("ammoUpMax", ammoUpMax);
        PlayerPrefs.SetInt("countW", countW);

        PlayerPrefs.Save();
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene().name;
        if (scene == "MenuScene")
        {
            notInMenuScene = false;
        }
        else
        {
            notInMenuScene = true;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Lizzard");
        if (enemies.Length > 0)
        {
            inFight = true;
        }
        else
        {
            inFight = false;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {

            if (inFight == false)
            {
                SaveData();
            }
        }

    }

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 60;
        GUIStyle logStyle = new GUIStyle();
        logStyle.fontSize = 40;
        if (notInMenuScene )
        {
            GUI.Box(new Rect(20, 20, 450, 75), "Волна: " + (countW - 1), myStyle);
        }
        
        if ((nearTorgash) && (inFight == false))
        {
            GUI.Box(new Rect(100, 90, 200, 75), "Нажмите 'K', чтобы открыть магазин", logStyle);
        }
    }

}
