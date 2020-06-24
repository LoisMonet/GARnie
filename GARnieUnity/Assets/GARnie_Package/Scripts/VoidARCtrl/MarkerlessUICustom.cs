using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Custom class of MarkerlessUI of VoidAR
/// </summary>
public class MarkerlessUICustom : MonoBehaviour
{
    /// <summary>
    /// Camera on Editor
    /// </summary>
    public GameObject camEdit;
    /// <summary>
    /// Camera when use Mobile
    /// </summary>
    public GameObject camMobile;
   
    public static int countreset = 0;

    void Awake()
    { //MAYBE GOOD TO DON'T CRASH
        
#if UNITY_EDITOR //WAITING
        camEdit.gameObject.SetActive(true);
        camMobile.gameObject.SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
				camEdit.gameObject.SetActive(false);
				camMobile.gameObject.SetActive(true);
#endif



    }

    void Start()
    {
        Reset();
    }


    /// <summary>
    /// CONTINUE TO MAKE TEST
    /// less crash with that and you can now put smartphone on pause without crash, now works sometimes WITHOUT continue to make test
    /// </summary>
    /// <param name="pauseStatus"></param>
    void OnApplicationPause(bool pauseStatus)
    {
        //Reset();
        //			if (pauseStatus) {
        //				
        //
        //				if (this.GetComponent<Camera> ().enabled) {
        //					
        //					this.GetComponent<Camera> ().enabled = false;
        //
        //				}
        //			} else {
        //				if (!this.GetComponent<Camera> ().enabled){ 
        //					this.GetComponent<Camera> ().enabled = true;
        //				}
        //
        //
        //			}




    }

    /// <summary>
    /// Show information to debug
    /// Don't clean currently , because can be useful to use comment code to improve code of app
    /// </summary>
    void OnGUI()
    {

        SceneAssetCtrl.instance.extraInfo.text = //for dev extra info
                   //	"Scale go: " + goScale.transform.localScale +
                   //"\nScale Pikachu: " + bb.transform.localScale +
                   //			"\ntranslation GO: " + bb.transform.root.position +
                   //			//"\nrotation GO: " + bb.transform.root.rotation.eulerAngles +
                   //			"\ntranslation pikachu: " + bb.transform.parent.GetChild (3).localPosition +
                   //			"\ntranslation Ho-Oh: " + bb.transform.parent.GetChild (2).localPosition +
                   //			"\ntranslation char: " + bb.transform.parent.GetChild (1).localPosition +
            "\nscreen size : " + Screen.width + "x" + Screen.height +
        //"\nGUI: " + raycastmenu.touchGUI +
        //"\ntouchID : " + RotateScale.positiononscreen+
        //"\nerror : " + MarkerlessTracking.lastStateString+
        //"\ny pos : " +MarkerlessTracking.posYtrackingallgood+
        //"\nGui status : " +SimpleDrag.statusGUI+
        //"\nSilkke active : " +bb.transform.parent.GetChild (4).gameObject.activeSelf+"|"+bb.transform.parent.GetChild (4).GetChild(bb.transform.parent.GetChild (4).childCount-1).name+
        //"\nerror :|active oo: "+bb.transform.root.GetChild(0).gameObject.activeSelf+
        //"\nspeed :"+  AvatarCharacterController.speed2+ //WAITING
        "\nerror :" + MarkerlessTrackingCustom.state2 +
        //"\nstationnaire :" + AvatarCharacterController.statio2 +
        //"\nposition :" + AvatarCharacterController.movedir2 +
        //			"\ntextures :" + AssetLoader.allPath+                       //REMOVE TEMP
        //			if(GameObject.Find (RotateScale.getAvatarName)!=null){
        //				txt.text += "\nscale :" + GameObject.Find (RotateScale.getAvatarName).transform.localScale;
        //				txt.text += "\ntranslate :" + GameObject.Find (RotateScale.getAvatarName).transform.localPosition;
        //			}
        //				//"\nmenuornot: "+Menu.menuornot+
        //			txt.text+=
        "\nOops, you are in dev info. " +
            "\nTap again on battery icon to remove this function.";
        //"\nscreenPos : " + RotateScale.positiononscreen+" | "+RotateScale.openmenu+
        //	"\nmenuButton|menuornot|boolmennubutton2 : " + menu_button.boolmenubutton+" | "+Menu.menuornot+" | "+menu_button.boolmenubutton2;
        //"\nnear clip plane: " + Camera.main.nearClipPlane;
        //	"\ntranslation camera: " + arCamera.transform.localPosition;
        //+ "\nlastcallgo: " + threedmodelbutton.lastcallGO
        //+ "\nlibassimp enabled: " + enabled
        //+ "\nactive or not car: " + bb.transform.parent.GetChild (0).gameObject.activeSelf;
        //+"\ncar wrapper ok or not: " + click_path.formeshassimp;



        var btnHeight = Screen.height * 0.1f;
        var btnWidth = btnHeight * 3.0f;
       // var gap = 20;
        GUI.skin.button.fontSize = 36;
        //	        if (GUI.Button(new Rect(0, gap, btnWidth, btnHeight), "Start"))
        //	        {
        ////				if (open) {
        ////					VoidAR.GetInstance ().startMarkerlessTracking (); 
        ////					Invoke ("firstresVAR", 0.3f);	
        ////					Invoke ("firststartVAR", 0.3f);	
        ////					open = false;
        ////					print ("test41");
        ////				}else {
        //					VoidAR.GetInstance().startMarkerlessTracking();
        //
        //				//bb.transform.root.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 6f));
        //				print ("name "+bb.transform.root.name);
        //
        //			//	}
        //	            
        //
        //	        }


        /* if (!Application.isMobilePlatform) {
            GUI.color = Color.red;
            GUI.Label(new Rect(50, 0, Screen.width - 100, 60), "仅支持iOS、Android设备运行！");
        }
       */
    }
    
    public void Reset()
    {

        for (int b = 0; b < SceneAssetCtrl.instance.modelsToTrack.transform.childCount; ++b)
        { //Destroy clones
            if (SceneAssetCtrl.instance.modelsToTrack.transform.GetChild(b).name.Contains(SceneAssetCtrl.CLONEIDENTIFIER))
            {
                Destroy(SceneAssetCtrl.instance.modelsToTrack.transform.GetChild(b).gameObject);
            }
        }
        for (int b = 0; b < SceneAssetCtrl.instance.modelsToTrack.transform.childCount; ++b)
        { //reset and active false all existing objects

            SceneAssetCtrl.instance.modelsToTrack.transform.GetChild(b).localPosition = Vector3.zero; //to get good start tracking
           
            if ((SceneAssetCtrl.instance.modelSilkkeAsset != null) && SceneAssetCtrl.instance.modelsToTrack.transform.GetChild(b).gameObject.activeSelf && SceneAssetCtrl.instance.modelsToTrack.transform.GetChild(b).name.Equals(SceneAssetCtrl.instance.modelSilkkeAsset.name))
            { //to reset Avatar values
                foreach (Transform tr in SceneAssetCtrl.instance.modelSilkkeAsset.transform)
                {
                    if (tr.name.Equals(RotateScale.getAvatarName))
                    {

                        tr.localPosition = Vector3.zero;
                        tr.localRotation = Quaternion.identity;
                        tr.localScale = new Vector3(1f, 1f, 1f); //keep silkke on the ground
                        tr.localPosition = new Vector3(0f, 0f, 0f); //keep silkke on the ground
                    }
                }
            }
            SceneAssetCtrl.instance.modelsToTrack.transform.GetChild(b).gameObject.SetActive(false);




        }

        SceneAssetCtrl.instance.modelsToTrack.transform.root.localPosition = new Vector3(0, 0, 0); //to get good start, but maybe useless
        countreset++;

        if (SceneAssetCtrl.instance.modelImportPanel.gameObject.activeSelf)
        { //hide YourModels if reset
            SceneAssetCtrl.instance.modelImportPanel.gameObject.SetActive(false);

            SceneAssetCtrl.instance.modelAnimPanel.transform.localScale = Vector3.one;
        }

        if (SceneAssetCtrl.instance.silkkeConnectBtn != null)
        {
            SceneAssetCtrl.instance.silkkeConnectBtn.transform.gameObject.SetActive(true);  // show Silkke connector if you reset
        }
       
#if UNITY_EDITOR

#elif UNITY_ANDROID || UNITY_IOS
			VoidAR.GetInstance().resetMarkerless();
#endif
    }

}
