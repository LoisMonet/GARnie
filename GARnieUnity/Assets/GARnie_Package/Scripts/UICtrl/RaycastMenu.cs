using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// To manage if you click on Menu or not to know if you interact with the menu or with 3d models
/// </summary>
public class RaycastMenu : MonoBehaviour
{

    /// <summary>
    /// Don't have 2 cond in same time
    /// </summary>
    bool touch = false;
    /// <summary>
    /// for GUI
    /// </summary>
    public static string touchGUI;

    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch fing = Input.GetTouch(0);
            if (fing.phase == TouchPhase.Began)
            {
                IsPointer();

            }
        }


    }

    /// <summary>
    /// useful to get good menu and menu button experience(don't change scene when one of them clicked)
    /// </summary>
    void IsPointer()
    {
        if (Input.touchCount > 0)
        {


            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                if (touch == false)
                {
                    Menu.menuornot = true;
                    touch = true;
                }
            }
            else
            {
                if (touch == true)
                {
                    Menu.menuornot = false;
                    touch = false;
                }
            }
            touchGUI = "touch: " + touch + ",menuornot: " + Menu.menuornot;
        }

    }
}
