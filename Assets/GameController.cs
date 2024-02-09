using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject foxPrefab;
    public GameObject monsterPrefab;
    private GameObject currentCharacter;
    public TextMeshProUGUI scoreText;
    private int stage;
    public GameObject win_button;
    public GameObject pause_button;
    private List<GameObject> initialFoxes = new List<GameObject>();
    private bool hasGeneratedMonster = false;
    private GameObject newMonster = null;
    void Start()
    {
        Time.timeScale = 1.0f;

        for(int i=0; i<3; i++)
        {
            GameObject newFox = SpawnFox();
            initialFoxes.Add(newFox);
        }

        stage = 1;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) // 遊戲暫停
        {
            Time.timeScale = 0.0f;
            pause_button.SetActive(true);
        }
        
        if(stage == 1)
        {
            removeInitialFoxes();
        }

        if(initialFoxes.Count == 0 && stage == 1) //進到第二關
        {
            stage = 2;
            updateStage();

            if (stage == 2 && !hasGeneratedMonster)
            {
                newMonster = SpawnMonster();
                hasGeneratedMonster = true;
            }
        }

        if(stage == 2)
        {
            GameObject[] foxes = GameObject.FindGameObjectsWithTag("Fox");
            if(newMonster==null && foxes.Length==0)
            {
                WinButton();
            }
        }
    }

    public GameObject SpawnMonster()
    {
        float randomX = Random.Range(-8.1f, 8.15f);
        GameObject newMonster = Instantiate(monsterPrefab, new Vector3(randomX, 0, 0), Quaternion.identity);
        newMonster.SetActive(true);

        return newMonster;
    }

    public GameObject SpawnFox()
    {
        float randomX = Random.Range(-8.1f, 8.15f);
        GameObject newFox = Instantiate(foxPrefab, new Vector3(randomX, 0, 0), Quaternion.identity);
        newFox.SetActive(true);

        return newFox;
    }

    private void removeInitialFoxes()
    {
        for(int i = initialFoxes.Count - 1; i >= 0; i--)
            {
                if (initialFoxes[i] == null)
                {
                    initialFoxes.RemoveAt(i);
                }
            }
    }
    private void updateStage()
    {
        scoreText.text = "STAGE: " + stage.ToString();
    }
    private void WinButton()
    {
        Time.timeScale = 0.0f;
        win_button.SetActive(true);
    }
    public void PauseButton()
    {
        //遊戲繼續
        pause_button.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

