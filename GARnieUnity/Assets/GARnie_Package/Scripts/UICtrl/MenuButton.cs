using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// To manage button to display Menu on editor
/// Hidden on Android
/// </summary>
public class MenuButton : MonoBehaviour
{
    /// <summary>
    /// Image of the button menu
    /// </summary>
    Image image;
    /// <summary>
    /// Pour lancer le menu (une fois)    getkey call during 0.1f
    /// </summary>
    public static bool boolmenubutton = false;
    /// <summary>
    /// Pour quitter le menu (une fois)  getkey call during 0.1f
    /// </summary>
    public static bool boolmenubutton2 = false;
    /// <summary>
    /// Menu open at start for 2 0 else
    /// </summary>
    public static int j = 0;
    void Start()
    {

#if UNITY_EDITOR || UNITY_IOS//NOW MENU BUTTON WORKS JUST ON EDITOR,CHANGE THAT SOON
        this.gameObject.SetActive(true);
        boolmenubutton2 = true; //I TEST
        image = GetComponent<Image>();
        var tempcolor = image.color;
        tempcolor.a = 0f;
        image.color = tempcolor;
#elif UNITY_ANDROID
			//#elif UNITY_ANDROID || UNITY_IOS
				this.gameObject.SetActive(false);
#endif



    }


    public void OnButtonEditor()
    {
        Menu.menuornot = true;
        j++;
        if (j % 3 == 1)
        {
            image = GetComponent<Image>();
            var tempcolor = image.color;
            tempcolor.a = 1f;
            image.color = tempcolor;
        }
        else if (j % 3 == 2)
        {
            boolmenubutton = true;
        }
        else
        {
            boolmenubutton2 = true;
            image = GetComponent<Image>();
            var tempcolor = image.color;
            tempcolor.a = 0f;
            image.color = tempcolor;
        }
        Invoke("BoolButtonFalse", 0.1f);   //getkey call during 0.1f 
    }

    public void BoolButtonFalse()
    {   //IT'S MAYBE USELESS
        boolmenubutton = false;
        boolmenubutton2 = false;
    }

}
