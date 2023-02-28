using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventCtrl : MonoBehaviour
{
    private void Awake() {
        transform.GetComponent<SpriteRenderer>().sprite = null;
    }
    public void OnAppearingEnd()
    {
        PlayerCtrl.Ins.PlayerMovement.ApplyGravity();
    }
}
