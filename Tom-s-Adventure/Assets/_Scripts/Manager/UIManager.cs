using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject pauseDialog;
    

    protected override void Awake() {
        this.MakeSingleton(false);
    }


}
