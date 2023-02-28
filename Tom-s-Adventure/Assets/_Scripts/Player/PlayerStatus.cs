using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : Singleton<PlayerStatus>
{

    private bool isDead = false;
    public bool IsDead => isDead;
    protected Rigidbody2D rb;
    protected Animator anim;

    protected override void Awake()
    {
        this.MakeSingleton(false);
    }
    protected override void Start()
    {
        base.Start();
        rb = PlayerCtrl.Ins.GetComponent<Rigidbody2D>();
        anim = PlayerCtrl.Ins.Model.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Finish"))
    //     {
    //         rb.bodyType = RigidbodyType2D.Static;
    //     }
    // }

    private void Die()
    {
        isDead = true;
        anim.SetBool("dead", isDead);
        anim.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        AudioManager.Ins.PlaySFX(EffectSound.DeathSound);
        StartCoroutine(WaitForRespawn());
    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(1f);
        isDead = false;
        anim.SetBool("dead", isDead);
        // anim.SetTrigger("respawn");
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
