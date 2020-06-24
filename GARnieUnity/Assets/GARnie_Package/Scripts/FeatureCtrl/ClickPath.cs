using UnityEngine;
using UnityEngine.UI;
using System;


/// <summary>
/// Manage Quit button and Silkke Button
/// </summary>
public class ClickPath : MonoBehaviour
{

    /// <summary>
    /// panel with anims of 3d model animated
    /// </summary>
    public GameObject manageanimfile;

    


    /// <summary>
    /// When tap on a model button
    /// </summary>
    public void QuitButton()
    {

        if (transform.parent.gameObject.activeSelf)
        {
            transform.parent.gameObject.SetActive(false);

            manageanimfile.transform.localScale = Vector3.one;
            if ((SceneAssetCtrl.instance.modelSilkkeAsset != null))
            {
                SceneAssetCtrl.instance.silkkeConnectBtn.transform.gameObject.SetActive(true);  // show Silkke connector if you play all except car
            }
        }
    }

    

}

