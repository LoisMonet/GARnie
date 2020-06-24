using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour {
    private Sprite StartImage;
    public Sprite StopImage;
    private Image image;
    private VideoRecordBehaviour vrb;
	void Awake () {
        image = GetComponent<Image>();
        StartImage = image.sprite;
        GetComponent<Button>().onClick.AddListener(OnClickHandler);
        vrb = Camera.main.GetComponent<VideoRecordBehaviour>();
        //录制完成生成视频文件成功事件
        vrb.AddEventListener(VoidAREvent.COMPLETE, onComplete);
    }

    void onComplete(VoidAREvent evt) {
        Debug.Log("Record onComplete:" + evt.data);
    }

    // Update is called once per frame
    void OnClickHandler () {
        if (!vrb.isRecording)
        {
            vrb.StartRecording();
            image.sprite = StopImage;
        }
        else {
            vrb.StopRecording();
            image.sprite = StartImage;
        }
	}
}
