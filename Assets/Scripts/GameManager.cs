using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject Player;

    [SerializeField] private GameObject VictoryUI;
    [SerializeField] private GameObject DefeatUI;

    private void Update()
    {
        int EnemiesLeft = Enemies.Length;
        foreach (GameObject enemy in Enemies)
        {
            if (enemy == null)
            {
                EnemiesLeft--;
            }
        }

        if (EnemiesLeft == 0)
        {
            Debug.Log("Player Won");
            VictoryUI.SetActive(true);
            Invoke("quitGame", 3);
        }
        else if (Player == null)
        {
            Debug.Log("Player Lost");
            DefeatUI.SetActive(true);
            Invoke("quitGame", 3);
        }
    }

    private void quitGame()
    {
        Application.Quit();
    }
}
