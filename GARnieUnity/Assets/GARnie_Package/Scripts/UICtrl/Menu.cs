using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// To manage menu
/// To take Picture when tap on back button
/// </summary>
public class Menu : MonoBehaviour
{

    /// <summary>
    /// Menu
    /// </summary>
    public GameObject menu;

    /// <summary>
    /// To use a tuto UI
    /// </summary>
    public GameObject tutostart;
    /// <summary>
    /// panel with anims of 3d model animated
    /// </summary>
    public Transform PanTexMan;

    /// <summary>
    /// If true can't change 3d model,rotate,scale because in menu
    /// </summary>
    public static bool menuornot = false;

    // Use this for initialization
    void Start()
    {
        menu.gameObject.transform.localScale = new Vector3(0, 0, 0);

        foreach (Transform go in PanTexMan)
        {  //no texture
            if (go.gameObject.name.Contains(SceneAssetCtrl.FILEIDENTIFIER))
            {
                go.gameObject.SetActive(false);
            }
        }
        foreach (Transform go in PanTexMan)
        {  //no anim
            if (go.gameObject.name.Contains(SceneAssetCtrl.ANIMIDENTIFIER))
            {
                go.gameObject.SetActive(false);
            }
        }
    }


    /// <summary>
    /// Unused
    /// </summary>
    public void RemoveTuto()
    { //tuto very simplified
        if (tutostart.gameObject != null)
        {
            //print ("1");
            Destroy(tutostart.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {


        if ((SceneAssetCtrl.instance.modelImportAsset != null) && ThreeDModelButton.lastcallGO.Contains(SceneAssetCtrl.instance.modelImportAsset.name))
        { //to watch textures just with OBJ MODELS


            if (GameObject.FindGameObjectsWithTag("texture").Length == 0)
            {
                foreach (Transform go in PanTexMan)
                {  //show texture
                    if (go.gameObject.name.Contains(SceneAssetCtrl.FILEIDENTIFIER))
                    {
                        go.gameObject.SetActive(true);
                    }
                }
                foreach (Transform go in PanTexMan)
                {  //show anim because not OBJ
                    if (go.gameObject.name.Contains(SceneAssetCtrl.ANIMIDENTIFIER))
                    {
                        go.gameObject.SetActive(false);
                    }
                }


            }

        }
        else if (!ThreeDModelButton.lastcallGO.Contains(" "))
        { //because at start lastcallGo=" "
            if (GameObject.FindGameObjectsWithTag("anim").Length == 0)
            {

                foreach (Transform go in PanTexMan)
                {   //remove texture
                    if (go.gameObject.name.Contains(SceneAssetCtrl.FILEIDENTIFIER))
                    {
                        go.gameObject.SetActive(false);
                    }
                }
                foreach (Transform go in PanTexMan)
                {  //show anim because not OBJ
                    if (go.gameObject.name.Contains(SceneAssetCtrl.ANIMIDENTIFIER))
                    {
                        go.gameObject.SetActive(true);
                    }
                }
                if (PanTexMan != null)
                { //else file appear at start
                }
            }

        }
#if UNITY_EDITOR || UNITY_IOS     //test menu on unity editor 

        bool getkey = MenuButton.boolmenubutton;   //true menu appear false menu disappears
#elif UNITY_ANDROID
			//#elif UNITY_ANDROID || UNITY_IOS
			//bool getkey =  Input.GetKeyDown (KeyCode.Menu);
			bool getkey =MenuButton.boolmenubutton ;
#endif

        //print(menu.GetComponent<RectTransform>().anchoredPosition.y); //test
        //print(menu_button.boolmenubutton+" M51");
        if (getkey)
        {
            RemoveTuto();

            menu.gameObject.transform.localScale = new Vector3(1, 1, 1); //permits continue to use speedz and speedanim values but without use setactive(false) 
            menuornot = true;
            MenuButton.boolmenubutton = false; //I TEST
            MenuButton.boolmenubutton2 = false; //I TEST

        }
        if (menu.gameObject.transform.localScale.x == 0)
        {
            menuornot = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            { //to take a picture
                Rect rect = new Rect(0, 0, Screen.width, Screen.height);
                string fileName = DateTime.Now.Ticks + ".jpg";

                StartCoroutine(TakePicture.TakeScreenshot(rect, fileName, 1)); //without raw image
            }
        }


    }


}

