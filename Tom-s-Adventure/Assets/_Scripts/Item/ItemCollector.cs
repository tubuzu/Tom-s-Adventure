// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int cherries = 0;

    [SerializeField] private Text cherriesText;

    private void Start() {
        this.cherriesText = GameObject.Find("CherriesCountTxt").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            AudioManager.Ins.PlaySFX(EffectSound.CollectItemSound);
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }
}