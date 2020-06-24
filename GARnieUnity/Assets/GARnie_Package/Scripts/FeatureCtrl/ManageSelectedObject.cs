using Lean.Touch;
using UnityEngine;

/// <summary>
/// To manage translate, rotate and scale with 3d models
/// </summary>
public class ManageSelectedObject : MonoBehaviour
{
    /// <summary>
    /// 0:Lean.Touch.LeanTranslateSmooth>() not yet enabled | 1:else
    /// </summary>
    public int before;

    /// <summary>
    ///  Object selected when start by default
    /// </summary>
    void Start()
    {
        before = 0;
        GetComponent<LeanTranslateSmooth>().enabled = false;
        GetComponent<LeanScaleSmooth>().enabled = false;
        GetComponent<RotateScale>().enabled = false;
        GetComponent<LeanSelectable>().Select();
        //print(name + before + "|" + GetComponent<Lean.Touch.LeanSelectable>().IsSelected);

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<LeanSelectable>().IsSelected && before == 0)
        {
            AllPossessLeanSelectable();
            GetComponent<LeanSelectable>().Select();
            GetComponent<LeanTranslateSmooth>().enabled = true;
            GetComponent<LeanScaleSmooth>().enabled = true;
            GetComponent<RotateScale>().enabled = true;

            before = 1;


        }


    }


    void AllPossessLeanSelectable()
    {
        foreach (LeanSelectable ls in GameObject.FindObjectsOfType<LeanSelectable>())
        {
            ls.GetComponent<LeanSelectable>().Deselect();
            ls.GetComponent<LeanTranslateSmooth>().enabled = false;
            ls.GetComponent<LeanScaleSmooth>().enabled = false;
            ls.GetComponent<RotateScale>().enabled = false;
            ls.GetComponent<ManageSelectedObject>().before = 0;
        }
    }
}
