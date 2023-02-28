using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] protected int currentLevel;
    public bool pause = false;
    public int CurrentLevel { get => currentLevel; }
    public bool levelCompleted = false;
    protected override void Awake()
    {
        this.MakeSingleton(false);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    protected override void Start()
    {
        base.Start();
        AudioManager.Ins.PlayMusic(MusicSound.BackgroundSound);
    }
    public void PauseGame() {
        Time.timeScale = 0;
        pause = true;
        AudioManager.Ins.StopCurrentMusic(true);
        UIManager.Ins.pauseDialog.SetActive(true);
    }
    public void ResumeGame() {
        Time.timeScale = 1;
        pause = false;
        AudioManager.Ins.StopCurrentMusic(false);
        UIManager.Ins.pauseDialog.SetActive(false);
    }
    public void Quit() {
        Application.Quit();
    }
    public void Home() {
        Time.timeScale = 1;
        pause = false;
        SceneManager.LoadScene("Level Selection");
    }
}
