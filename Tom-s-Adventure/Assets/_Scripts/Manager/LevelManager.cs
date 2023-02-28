using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<Transform> LevelButtons;
    public int reachedLevel;
    private void Awake()
    {
        LevelButtons = new List<Transform>();
        reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);
        // if(PlayerPrefs.GetInt("Level") >= 2) {
        //     reachedLevel = PlayerPrefs.GetInt("Level");
        // }
        for (int i = 0; i < transform.childCount; i++)
        {
            LevelButtons.Add(transform.GetChild(i).transform);
            LevelButtons[i].GetComponent<Button>().GetComponentInChildren<Text>().text = (i + 1).ToString("00");
            LevelButtons[i].GetComponent<LevelButton>().level = i + 1;
            LevelButtons[i].GetComponent<LevelButton>().SetStar(PlayerPrefs.GetInt("LevelStar_" + (i + 1).ToString("00"), 0));
            if (i + 1 > reachedLevel)
            {
                LevelButtons[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    // public void LoadScene(int level) {
    //     PlayerPrefs.SetInt("CurrentLevel", level);
    //     Application.LoadLevel("Loading");
    // }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("ReachedLevel", PlayerPrefs.GetInt("ReachedLevel") + 1);
        reachedLevel++;
        SceneManager.LoadScene("Level_" + reachedLevel.ToString("00"));
    }
}
