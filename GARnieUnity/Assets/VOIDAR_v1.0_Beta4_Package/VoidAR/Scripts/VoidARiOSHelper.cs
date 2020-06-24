using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.IO;
using UnityEngine;
#if UNITY_IOS
public class VoidARiOSHelper : IVoidARHelper
{
    const string dllName = "__Internal";
    [DllImport(dllName)]
	private static extern void _initBGTexture(ref long textureY, ref long textureCbCr, ref int width, ref int height,int graphicsDeviceType);
    [DllImport(dllName)]
	private static extern void _initLibrary(int type, int trackmax, bool isMirror, float scenescale,int focusMode);
    [DllImport(dllName)]
    private static extern void _getFOV(int width, int height, ref float fovx, ref float scalex, ref float scaley);
    [DllImport(dllName)]
    private static extern void _openCamera(int cameraIndex, ref int opened);
    [DllImport(dllName)]
    private static extern void _closeCamera();
    [DllImport(dllName)]
    private static extern void _startCapture();
    [DllImport(dllName)]
    private static extern void _stopCapture();
    [DllImport(dllName)]
    private static extern void _getMatchResult(IntPtr frame, ref int width, ref int height, ref int flip, double[] matrix, byte[] markers, ref int mainMarker, int[] scores);
    [DllImport(dllName)]
    private static extern void _unloadResource();
    [DllImport(dllName)]
    private static extern void _match();

    [DllImport(dllName)]
    private static extern void _reset();
    [DllImport(dllName)]
    private static extern void _getTrackStatus(int[] status);
	

    [DllImport(dllName)]
    private static extern void _addCurrentShapeImageTarget(ref int success, byte[] name, int tracking);
    [DllImport(dllName)]
    private static extern IntPtr _getNewCloudTarget(ref int newTarget, ref int width, ref int height, ref int size, ref int imgType, byte[] urls, byte[] names, byte[] matadata);
    [DllImport(dllName)]
    private static extern void _setUser(string accessKey, string secretKey, int useCloud, int privacy, string ip, int port, string api);
    [DllImport(dllName)]
    private static extern void _setShapeMatchLevel(int lvl);
    [DllImport(dllName)]
    private static extern bool _checkNet();
    [DllImport(dllName)]
    private static extern bool _useExtensionTracking(bool use);
   // [DllImport(dllName)]
   // private static extern bool _addTarget(string path, byte[] data, int width, int height);
    [DllImport(dllName)]
    private static extern bool _addTarget(string path, int pathType, float width);
    [DllImport(dllName)]
    private static extern bool _addBinaryTarget(string path, float width);
    [DllImport(dllName)]
    private static extern bool _finishAddTarget();
    [DllImport(dllName)]
    private static extern bool _removeImageTarget(string targetName);

    //VideoPlay
    private IntPtr videoPlayerPtr = IntPtr.Zero;
    [DllImport(dllName)]
    private static extern void setVideoURL(IntPtr videoPlayerPtr, string videoURL);
    [DllImport(dllName)]
    private static extern int updateVideoTexture(IntPtr videoPlayerPtr, int textureID);

    [DllImport(dllName)]
    private static extern void play(IntPtr videoPlayerPtr);
    [DllImport(dllName)]
    private static extern void pauseVideo(IntPtr videoPlayerPtr);
    [DllImport(dllName)]
    private static extern void onExit(IntPtr videoPlayerPtr);
    [DllImport(dllName)]
    private static extern int seekTo(IntPtr videoPlayerPtr, float position);

    [DllImport(dllName)]
    private static extern IntPtr initVideoPlayer();

    [DllImport(dllName)]
    private static extern int getState(IntPtr videoPlayerPtr);
    [DllImport(dllName)]
    private static extern int getWidth(IntPtr videoPlayerPtr);
    [DllImport(dllName)]
    private static extern int getHeight(IntPtr videoPlayerPtr);
    
    [DllImport(dllName)]
    private static extern int videoGetDuration(IntPtr videoPlayerPtr);

    [DllImport(dllName)]
    private static extern int videoGetCurrentPosition(IntPtr videoPlayerPtr);

    [DllImport(dllName)]
    private static extern void videoSetVolume(IntPtr videoPlayerPtr,float volume);

    [DllImport(dllName)]
    private static extern float videoGetVolume(IntPtr videoPlayerPtr);

    [DllImport(dllName)]
    private static extern void videoSetAutoPlay(IntPtr videoPlayerPtr,bool b);

    [DllImport(dllName)]
    private static extern bool videoGetAutoPlay(IntPtr videoPlayerPtr);

    [DllImport(dllName)]
    private static extern bool videoIsPlaying(IntPtr videoPlayerPtr);
    //[DllImport(dllName)]
    //private static extern void _setAPPKey(string appkey, ref int result);

    //VideoRecord
    [DllImport(dllName)]
    private static extern void startCapturing(string videoName,
    int frameWidth, int frameHeight, int frameRate,
    int glTextureID);

    [DllImport(dllName)]
    private static extern int stopCapturing(int action);

    [DllImport(dllName)]
    private static extern void captureFrame();

    [DllImport(dllName)]
    private static extern void _startMarkerlessTracking(ref int errorCode);
    [DllImport(dllName)]
    private static extern void _resetMarkerless(ref int errorCode);

    [DllImport(dllName)]
    private static extern void _saveImageTargetDescriptor(string path);



    [DllImport(dllName)]
    private static extern void _startProfile();
    [DllImport(dllName)]
    private static extern void _stopProfile();

    [DllImport(dllName)]
    private static extern IntPtr _onCameraFrame(ref int width, ref int height, ref int size,ref int imgType);

    [DllImport(dllName)]
    private static extern void setMirror(bool isMirror);

    [DllImport(dllName)]
    private static extern void _setAPPKey(string appkey, ref int result);

    [DllImport(dllName)]
    private static extern void _environmentalLight(ref float intensity);

    [DllImport(dllName)]
    private static extern void _setFlashTorchMode(bool open);

    [DllImport(dllName)]
    private static extern void _initVideoTexture(ref long _texture, ref int _width, ref int _height, int _graphicsDeviceType );

	[DllImport (dllName)]
	private static extern IntPtr _getRenderEventFunc();

    // [DllImport(dllName)]
    // private static extern void _getPostionOnTarget(int inputX, int inputY, ref int outputX, ref int outputY);


    //[DllImport(dllName)]
    //private static extern void savePhotosAlbum(string imageFilePath); //和录屏同一个静态库

    /////////////////////////////////////////////////////
    //########## 摄像头、识别、跟踪接口 ##################
    /////////////////////////////////////////////////////

    public void saveImageTargetDescriptor(string path)
    {

        _saveImageTargetDescriptor(path);
    }



	public void Init(int markerType, bool enableExtensionTracking, int trackmax, bool isMirror, float sceneScale,int focusMode)
    {
		//Debug.Log ("sdfsdfdffdfsfs");
		//Debug.Log ("Markertype111: " + markerType );
		_initLibrary(markerType, trackmax,isMirror,sceneScale,focusMode);
        _useExtensionTracking(enableExtensionTracking);
    }

	public void InitBGTexture(ref long textureY, ref long textureCbCr, ref int width, ref int height,int graphicsDeviceType)
    {
		_initBGTexture(ref textureY, ref textureCbCr, ref width, ref height,graphicsDeviceType);
    }

    private ExternalTextureDesc videoETD = new ExternalTextureDesc { textureId = IntPtr.Zero };

    public void InitVideoTexture(ref long _texture, ref int _width, ref int _height, int _graphicsDeviceType)
    {
        if (videoETD.textureId == IntPtr.Zero)
        {
            _initVideoTexture(ref _texture, ref _width, ref _height, _graphicsDeviceType);
            videoETD.textureId = new IntPtr(_texture);
            videoETD.width = _width;
            videoETD.height = _height;
        }
        else
        {
            _texture = videoETD.textureId.ToInt64();
            _width = videoETD.width;
            _height = videoETD.height;
        }
    }

    public void GetFOV(int width, int height, ref float fovx, ref float scalex, ref float scaley)
    {
        _getFOV(width, height, ref fovx, ref scalex, ref scaley);
    }

    public bool OpenCamera(int cameraIndex, int cameraWidth, int cameraHeight, int focusMode)
    {
        int isOpen = 0;
        _openCamera(cameraIndex, ref isOpen);
        return isOpen == 1;
    }

    public void CloseCamera()
    {
        _closeCamera();
    }

    public void StartCapture()
    {
        _startCapture();
    }

    public void StopCapture()
    {
        _stopCapture();
    }

    public void GetMatchResult(IntPtr frame, ref int width, ref int height, ref int flip, double[] matrix, byte[] markers, ref int mainMarker, int[] scores)
    {
        _getMatchResult(frame, ref width, ref height, ref flip, matrix, markers, ref mainMarker, scores);
    }

    public void Match()
    {
        _match();
    }

    public void Reset()
    {
        _reset();
    }

    public int GetTrackingStatus()
    {
        int[] status = new int[1];
        _getTrackStatus(status);
        return status[0];
    }

    public byte[] GetNewCloudTarget(ref int newTarget, byte[] urls, byte[] names, byte[] matadata, ref int width, ref int height)
    {
        int size = 0;
        int imgType = 0;
        IntPtr source = _getNewCloudTarget(ref newTarget, ref width, ref height, ref size, ref imgType, urls, names, matadata);
        if (size > 0)
        {
            byte[] pixels = new byte[size];
            Marshal.Copy(source, pixels, 0, size);
            return pixels;
        }
        return null;
    }

    public void SetUser(string accessKey, string secretKey, int useCloud, int privacy, string ip, int port, string api)
    {
        _setUser(accessKey, secretKey, useCloud,privacy,ip,port,api);
    }

    public void SetShapeMatchLevel(int level)
    {
        _setShapeMatchLevel(level);
    }

    public bool HasNetworkConnection()
    {
        return _checkNet();
    }

    public void EnabledExtensionTracking(bool enabled)
    {
        _useExtensionTracking(enabled);
    }

    public void AddImageTarget(string path, byte[] data, int width, int height)
    {
       // _addTarget(path, data, width, height);
       // Debug.Log("width:" + width + "height:" + height);
    }

    public void AddImageTarget(string path, int pathType, float width)
    {
        _addTarget(path, pathType, width);
    }

    public void AddImageBinaryTarget(string path, float width)
    {
        _addBinaryTarget(path, width);
    }

    public void RemoveImageTarget(string targetName)
    {
        _removeImageTarget(targetName);
    }

    public void FinishAddImageTargets()
    {
        _finishAddTarget();
    }

    public void AddShapeImageTarget(ref int success, byte[] name, int tracking)
    {
        _addCurrentShapeImageTarget(ref success, name, tracking);
    }

    public void UnloadResource()
    {
        _unloadResource();
    }

    /////////////////////////////////////////////////////
    //########        视频播放 接口        ############
    /////////////////////////////////////////////////////
    private string lastVideoPlayURL;
    public void SetVideoPlayURL(string url)
    {
        lastVideoPlayURL = url;
        setVideoURL(getVideoPlayer(), url);
    }

    public void VideoPlay()
    {
        play(getVideoPlayer());
    }

    public void VideoPause()
    {
        pauseVideo(getVideoPlayer());
    }

    public void VideoPlayerExit()
    {
        onExit(getVideoPlayer());
        videoPlayerPtr = IntPtr.Zero;
        videoETD.textureId = IntPtr.Zero;
        lastVideoPlayURL = null;
    }

    public void VideoPlaySeekTo(float position)
    {
        seekTo(getVideoPlayer(), position);
    }

    public int GetVideoHeight()
    {
        return getHeight(getVideoPlayer());
    }

    public int GetVideoWidth()
    {
        return getWidth(getVideoPlayer());
    }

    public bool UpdateVideoTexture(int nativeTextureID)
    {
        int result = updateVideoTexture(getVideoPlayer(), nativeTextureID);
        return result != 0;
    }

    public int GetVideoPlayStatus()
    {
        return getState(getVideoPlayer());
    }

    IntPtr getVideoPlayer()
    {
        if (videoPlayerPtr == IntPtr.Zero)
        {
            videoPlayerPtr = initVideoPlayer();
        }
        return videoPlayerPtr;
    }

    public string GetVideoPlayURL()
    {
        return lastVideoPlayURL;
    }

    /// <summary>
    /// 视频长度
    /// </summary>
    /// <returns></returns>
    public int GetVideoDuration() {
        return videoGetDuration(getVideoPlayer());
    }
    /// <summary>
    /// 视频播放进度
    /// </summary>
    /// <returns></returns>
    public int GetVideoCurrentPosition() {
        return videoGetCurrentPosition(getVideoPlayer());
    }
    /// <summary>
    /// 设置视频音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetVideoVolume(float volume) {
        videoSetVolume(getVideoPlayer(), volume);
    }
    /// <summary>
    /// 获取视频音量大小
    /// </summary>
    /// <returns></returns>
    public float GetVideoVolume() {
        return videoGetVolume(getVideoPlayer());
    }
    /// <summary>
    /// 设置是否自动播放
    /// </summary>
    /// <param name="b"></param>
    public void SetVideoAutoPlay(bool b) {
        videoSetAutoPlay(getVideoPlayer(), b);
    }
    /// <summary>
    /// 获取是否自动播放
    /// </summary>
    /// <returns></returns>
    public bool GetVideoAutoPlay() {
        return videoGetAutoPlay(getVideoPlayer());
    }

    public bool VideoIsPlaying() {
        return videoIsPlaying(getVideoPlayer());
    }
    /////////////////////////////////////////////////////
    //############### 视频 录制接口        ##############
    /////////////////////////////////////////////////////

    public void VideoCaptureInit()
    {

    }

    public void StartVideoCapturing(string videoFileName, int frameWidth, int frameHeight, int frameRate, int textureId)
    {
        startCapturing(videoFileName, frameWidth, frameHeight, frameRate, textureId);
    }

    public void StopVideoCapturing()
    {
        stopCapturing(0); //0 保存到相册 1 保存到文档 2 丢弃
    }

    public void VideoCaptureFrame(int textureID)
    {
        captureFrame();
    }

    public void StartMarkerlessTracking(ref int errorCode)
    {
        _startMarkerlessTracking(ref errorCode);
    }

    public void ResetMarkerless(ref int errorcode)
    {
        _resetMarkerless(ref errorcode);
    }

    public void SaveScreenshot(string imageFilePath)
    {
        //savePhotosAlbum(imageFilePath);
    }


    public void StartProfile()
    {
        _startProfile();
    }

    public void StopProfile()
    {
        _stopProfile();
    }

    public byte[] GetFrameImage(ref int width, ref int height)
    {

        int size = 0;
        int imgType = 0;
        IntPtr source = _onCameraFrame(ref width, ref height, ref size, ref imgType);
        if (size > 0)
        {
            byte[] pixels = new byte[size];
            Marshal.Copy(source, pixels, 0, size);
            return pixels;
        }
        else
        {
            Debug.LogError("GetFrameImage size == 0");
            return null;
        }
    }

    public void SetCameraMirror(bool isMirror)
    {
        setMirror(isMirror);
    }

    public void SetAPPKey(string appkey, ref int result)
    {
        _setAPPKey(appkey, ref result);
    }

    public void GetEnvironmentalLight(ref float intensity)
    {
        _environmentalLight(ref intensity);
    }

    public void SetFlashTorchMode(bool open)
    {
        _setFlashTorchMode(open);
    }

	public IntPtr GetRenderEventFunc()
	{
		return _getRenderEventFunc ();
	}
}
#endif