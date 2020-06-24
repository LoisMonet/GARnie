using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Locate assets to be used with a singleton
/// </summary>
public class SceneAssetCtrl : MonoBehaviour
{
    public static SceneAssetCtrl instance=null;

    public List<GameObject> modelButtons;
    public List<GameObject> modelAssets;
    public Dictionary<string, GameObject> modelAssetsAndNames;
    /// <summary>
    /// Model selected with or wihtout anim
    /// Become Null as soon as check if this model has anim in menu found
    /// </summary>
    public static GameObject modelSelected=null;

    public GameObject modelSilkkeButton;
    public GameObject modelSilkkeAsset;
    public GameObject silkkeConnectBtn;
    public Dictionary<string, GameObject> modeSilkkeAssetsAndNames;

    public GameObject modelImportButton;
    public GameObject modelImportAsset;
    public Dictionary<string, GameObject> modeImportAssetsAndNames;

    public const string CLONEIDENTIFIER="Clone";
    public const string SUFFIXBUTTON = "Btn";
    public const string ANGLECLOSEIDENTIFIER = ">";
    public const string ANGLEOPENIDENTIFIER = "<";

    public const string FILEIDENTIFIER = "file[Maybe Removed]";
    public const string ANIMIDENTIFIER = "ModelAnim";

    /// <summary>
    /// Text content in Menu for model anim main title
    /// </summary>
    public const string MODELANIMMAINTITLE = "Model Anim";

    public GameObject menuGO;
    public GameObject menuChild;

    public GameObject canvasGO;

    public const string TRILIB = "Trilib";
    public const string SILKKE = "Silkke";
    public const string VOIDAR = "VOIDAR_v1.0_Beta4_Package";

    public GameObject importFilePathOriginal;
    /// <summary>
    /// Panel which contains model available to be imported using buttons
    /// </summary>
    public GameObject modelImportPanel;

    /// <summary>
    /// panel with anims of 3d model animated
    /// </summary>
    public GameObject modelAnimPanel;

    public GameObject modelsToTrack;
    public GameObject modelToTrackButtons;

    public Text menuTuto;
    /// <summary>
    /// Extra info to debug
    /// </summary>
    public Text extraInfo;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }else if(instance!=this){
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Wait 0.1f to check if Trilib or Silkke is available
    /// </summary>
    void Start()
    {

        Invoke("ModelSilkkeAssetCheck", 0.1f);

        Invoke("ModelImportAssetCheck", 0.1f);


    }

    public void ModelImportAssetCheck()
    {
        if (modelImportAsset == null)
        {
            Destroy(modelImportButton);

        }
    }

    public void ModelSilkkeAssetCheck()
    {
        if (modelSilkkeAsset == null)
        {

            Destroy(modelSilkkeButton);
            Destroy(silkkeConnectBtn);


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
