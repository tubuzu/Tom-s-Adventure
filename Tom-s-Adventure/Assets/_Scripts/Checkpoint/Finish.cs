using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !GameManager.Ins.levelCompleted)
        {
            AudioManager.Ins.PlaySFX(EffectSound.FinishSound);
            GameManager.Ins.levelCompleted = true;
            PlayerCtrl.Ins.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            PlayerPrefs.SetInt("LevelStar_" + GameManager.Ins.CurrentLevel.ToString("00"), PlayerCtrl.Ins.ItemCollector.cherries);
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
