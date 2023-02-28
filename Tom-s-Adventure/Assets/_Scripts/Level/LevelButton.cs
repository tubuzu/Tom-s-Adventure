using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int level;
    public void LoadLevel()
    {
        if (level == 0) return;
        PlayerPrefs.SetInt("CurrentLevel", level);
        SceneManager.LoadScene("Level_" + level.ToString("00"));
    }
    public void SetStar(int stars)
    {
        if (stars < 0 || stars > 3) return;
        Transform starContainer = transform.Find("Stars");
        HideStars();
        for (int i = 0; i < stars; i++)
        {
            starContainer.GetChild(i).gameObject.SetActive(true);
        }
    }
    protected void HideStars() {
        Transform starContainer = transform.Find("Stars");
        for (int i = 0; i < starContainer.childCount; i++)
        {
            starContainer.GetChild(i).gameObject.SetActive(false);
        }
    }
}
