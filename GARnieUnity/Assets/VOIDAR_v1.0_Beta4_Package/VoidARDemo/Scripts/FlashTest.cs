using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTest : MonoBehaviour {

    void OnGUI()
    {
        var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 3.0f;
        var gap = 20;
        GUI.skin.button.fontSize = 36;
        if (GUI.Button(new Rect(10, gap, btnWidth, btnHeight), "开启闪光"))
        {
            VoidAR.GetInstance().helper.SetFlashTorchMode(true);
        }
        if (GUI.Button(new Rect(10, Screen.height - btnHeight, btnWidth, btnHeight), "关闭闪光"))
        {
            VoidAR.GetInstance().helper.SetFlashTorchMode(false);
        }

    }
}
