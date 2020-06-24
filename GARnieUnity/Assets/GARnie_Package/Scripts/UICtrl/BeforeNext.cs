using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To manage button before and button next to change models in menu
/// </summary>
public class BeforeNext : MonoBehaviour
{
    /// <summary>
    /// index of Model button selected
    /// </summary>
    public int i;
    /// <summary>
    /// List of all buttons of model available
    /// </summary>
    List<GameObject> but;

    /// <summary>
    /// Wait 0.1f to check if Trilib is available
    /// </summary>
    void Start()
    {
        Invoke("ChooseModelAvailable",0.1f);
    }

    
    public void ChooseModelAvailable()
    {
        but = new List<GameObject>();
        foreach (GameObject go in SceneAssetCtrl.instance.modelButtons)
        {
            but.Add(go);
        }
        if ((SceneAssetCtrl.instance.modelSilkkeAsset != null))
        {
            
            if (SceneAssetCtrl.instance.modelSilkkeButton.transform.localScale != Vector3.zero && !but.Contains(SceneAssetCtrl.instance.modelSilkkeButton.gameObject)) //show silkke button just if you connect you one time or more
            {
                //print("oui");
                but.Add(SceneAssetCtrl.instance.modelSilkkeButton);
            }

        }
        if ((SceneAssetCtrl.instance.modelImportAsset != null))
        {

            but.Add(SceneAssetCtrl.instance.modelImportButton);
        }


        foreach (GameObject goBut in but)
        {

            goBut.gameObject.SetActive(false);
        }

        i = 0;
        but[i].gameObject.SetActive(true);
    }
    public void Next()
    {


        i++;
        if (i == but.Count)
        {
            i = 0;
        }
        for (int j = 0; j < but.Count; j++)
        {


            if (j == i)
            {
                but[j].gameObject.SetActive(true);
            }
            else
            {
                but[j].gameObject.SetActive(false);
            }
        }

    }

    public void Before()
    {


        i--;
        if (i == -1)
        {
            i = but.Count - 1;
        }

        for (int j = 0; j < but.Count; j++)
        {
            if (j == i)
            {
                but[j].gameObject.SetActive(true);
            }
            else
            {
                but[j].gameObject.SetActive(false);
            }
        }


    }
}
