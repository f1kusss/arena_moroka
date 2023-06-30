using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject uiPanel;
    public GameObject[] waves;
    private bool activePanel = false;
    public GameObject player;
    public GameObject hammer;
    public GameObject pistol;
    public GameObject auto;
    public GameObject pauseMenuUI;
    public bool GameIsPaused=false;
    // Start is called before the first frame update
    private LizzardController prefabScript;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager gameManager = GameManager.Instance;
            if ((gameManager.nearTorgash == false) && (gameManager.inFight == false))
            {
                prefabScript = prefab.GetComponent<LizzardController>();
                GameObject wave = waves[gameManager.countW - 1];
                if (wave != null)
                {
                    prefabScript.health += 50;
                    // Спавним префабы на каждой точке
                    foreach (Transform point in wave.transform)
                    {
                        Instantiate(prefab, point.position, Quaternion.identity);

                    }
                    gameManager.countW = gameManager.countW + 1;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager gameManager = GameManager.Instance;
            if ((gameManager.nearTorgash) && (gameManager.inFight == false))
            {
                if (activePanel == false)
                {
                    uiPanel.SetActive(true);
                    activePanel = true;
                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    uiPanel.SetActive(false);
                    activePanel = false;
                    Time.timeScale = 1f;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Pause();
            }
        }
    }
    public void Resume()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}