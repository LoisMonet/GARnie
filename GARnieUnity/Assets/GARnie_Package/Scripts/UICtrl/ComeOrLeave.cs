using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


/// <summary>
/// Don't understand
/// Display version
/// Use Tuto but unused currently
/// Displays an object when using the Markerless Tracking Method, that acts as a preview showing where the object will be placed.
/// </summary>
public class ComeOrLeave : MonoBehaviour, IPointerDownHandler
{

    /// <summary>
    /// value of finger on the screen
    /// </summary>
    public static Touch fing;

    /// <summary>
    /// Menu
    /// </summary>
    public GameObject menu;

    /// <summary>
    /// child of Menu
    /// </summary>
    public GameObject subMenu;
    /// <summary>
    /// Help button
    /// </summary>
    public GameObject help;
    /// <summary>
    /// assets under help button after clicked on
    /// </summary>
    public GameObject underHelp;
    /// <summary>
    /// useful for show underHelp or all menu buttons
    /// </summary>
    public static List<Transform> goForHelp;
    /// <summary>
    /// get current num version
    /// </summary>
    public Text numVersion;

    Animator anim_tut;

    /// <summary>
    /// true: add (beta) after version in Help menu, false else
    /// </summary>
    public bool betaOrNot = true;

    /// <summary>
    /// 0=empty 1=come=leave_tut 2=leave=come_tut 
    /// </summary>
    int leave = 0;

    void Start()
    {

        if (this.name.Equals(SceneAssetCtrl.instance.canvasGO.name))
        { //call numVersion just one time
            if (betaOrNot)
            {
                numVersion.text = "version(beta): " + Application.version; //if beta version
            }
            else
            {
                numVersion.text = "version: " + Application.version; //if production version
            }
        }
        goForHelp = new List<Transform>();
        anim_tut = GameObject.FindGameObjectWithTag("tut").GetComponent<Animator>(); //allow to find animator in panel
    }

    /// <summary>
    /// To quit Menu
    /// </summary>
    public void QuitMenu()
    {
        MenuButton.boolmenubutton2 = true;
    }

    /// <summary>
    /// Manage help menu
    /// </summary>
    public void HelpM()
    {



        if (leave != 2)
        {
            help.GetComponent<Selectable>().transition = Selectable.Transition.None; //to don't use Help like a button after clicking on it
            leave = 2;

            anim_tut.SetInteger("leave_tut", leave);
            for (int i = 2; i < subMenu.transform.childCount; ++i)
            {
                if (subMenu.transform.GetChild(i).gameObject.activeSelf)
                {
                    subMenu.transform.GetChild(i).gameObject.SetActive(false);//watch just help and underHelp
                }
                else
                {
                    goForHelp.Add(subMenu.transform.GetChild(i));
                }
            }
            underHelp.gameObject.SetActive(true); //show underHelp

        }


    }

    /// <summary>
    /// leave Help Menu
    /// </summary>
    public void BackHelp()
    {
        leave = 1;
        help.GetComponent<Selectable>().transition = Selectable.Transition.ColorTint;
        anim_tut.SetInteger("leave_tut", leave);
        underHelp.gameObject.SetActive(false); //hide underHelp
        for (int i = 2; i < subMenu.transform.childCount; ++i)
        {
            if (!goForHelp.Contains(subMenu.transform.GetChild(i)))
            { //watch which was active true and false before click on Help button
                subMenu.transform.GetChild(i).gameObject.SetActive(true);//watch all Menu
            }
        }


    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {  //allow don't change scene view if touch menu
        Menu.menuornot = true;

    }

    void Update()
    {



#if UNITY_EDITOR || UNITY_IOS     //test menu on unity editor 

        bool getkey2 = MenuButton.boolmenubutton2;
#elif UNITY_ANDROID
			//#elif UNITY_ANDROID || UNITY_IOS
			//bool getkey =  Input.GetKeyDown (KeyCode.Escape);
			bool getkey2 =MenuButton.boolmenubutton2 ;
#endif

        if (getkey2)
        {

            if (leave == 2)
            {
                anim_tut.SetInteger("leave_tut", 1);
            }
            menu.gameObject.transform.localScale = new Vector3(0, 0, 0); //allow to continue to use speedz and speedanim values but without use setactive(false) 
        }

#if UNITY_EDITOR || UNITY_IOS //to change track (1 or 2)

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //touch_click_target.i=1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //touch_click_target.i=0;
        }
#endif

    }

}




