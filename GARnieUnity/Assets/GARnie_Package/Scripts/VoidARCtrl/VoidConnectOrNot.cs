using UnityEngine;
using System.Collections;

/// <summary>
/// To know if your device works with VoidAR tracking
/// work: destroy this script
/// doesn't work: get a Toast Message
/// </summary>
public class VoidConnectOrNot : MonoBehaviour
{

    public void InvokeConnectOrNot()
    {
        if (!this.transform.GetChild(0).gameObject.activeSelf)
        {
            this.GetComponent<ToastExample2>().MethodToastExample(true, "Unfortunately your device is not available with this app");
        }
        else
        {

            Destroy(this); //destroy this script
        }
    }


    public void ConnectOrNot()
    {
       
        Invoke("InvokeConnectOrNot", 1);
    }
}

