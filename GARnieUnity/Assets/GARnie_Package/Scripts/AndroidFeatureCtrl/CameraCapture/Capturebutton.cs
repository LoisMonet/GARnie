using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
/// <summary>
/// Manage video taken
/// </summary>
public class Capturebutton : MonoBehaviour
{

    /// <summary>
    /// button to record
    /// </summary>
    public GameObject cameraRecord;
    public Text txt;
    public Capture capture;
    private GUIStyle style = new GUIStyle();
    private const string folderSave = "GARnie/";

    void Start()
    {

        cameraRecord.GetComponent<Image>().color = capture.isRunning ? new Color32(50, 146, 156, 143) : new Color32(50, 204, 156, 143);
        txt.text = capture.isRunning ? "Stop Recording" : "Start Recording";
        capture.fileName = folderSave + capture.fileName;


    }


    public void RecordButton()
    {

        if (capture.isRunning)
        {
            capture.StopCapturing();
            if (this.GetComponent<ToastExample2>() == null)
            {
                this.gameObject.AddComponent<ToastExample2>();
            }

            this.GetComponent<ToastExample2>().MethodToastExample(true, "video saved in" + ManageFile.GetDCIMPath() + folderSave);
            //te2=new ToastExample2(true,"video saved in /sdcard/DCIM");
        }
        else
        {
            capture.StartCapturing();
        }
        cameraRecord.GetComponent<Image>().color = capture.isRunning ? new Color32(50, 146, 156, 143) : new Color32(50, 204, 156, 143);
        txt.text = capture.isRunning ? "Stop Recording" : "Start Recording";
    }


}