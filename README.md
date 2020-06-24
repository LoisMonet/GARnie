# Public part of my AR app GARnie

This public project based on GARnie app on Play store: https://play.google.com/store/apps/details?id=eu.ModeLolito.ML_AllinAR&hl=fr <br />


Project made on Unity 2019.2.17f1 <br />

You can use the Unity project(/GARnieUnity) or use unity package(/Packages/GARnie_4_43_Public) which contains the project <br />

You can also add /Packages/Trilib_1_9_GARnie_Public if you bought Trilib plugin <br />
This package allows to use GARnie project with Trilib <br />
About Trilib , it's a very good plugin to import and export 3d contents at runtime <br />
Link : https://assetstore.unity.com/packages/tools/modeling/trilib-model-loader-package-91777 <br />

To use on Android , you need to switch platform "Android" <br />
App works on Orientation: "Landscape Left" <br />

The scene to use project is in /GARnie_Package/Scenes/GARnie <br />

You can add your own 3d models and buttons <br />
To add 3d models, put his game object in a "Container" Prefab . The path of this prefab is /GARnie_Package/Prefabs <br />
Then add "Container" in "ModelsToTrackChildChild" in the scene hierarchy <br />
To add button linked with this model, you can use "ModelBtnTmPro" in /GARnie_Package/Prefabs <br />
Then add this prefab in "ModelToTrackButtonsChild" in the scene hierarchy <br />
Finally go to on "SceneAssetCtrl" in the scene hierarchy and add model and button in "model Assets" and "model Buttons" <br />
It's important to use the same index for model and button to get the good connection when tap in a button <br />

Now you can play on editor or Android to use app <br />

---------------
About markeless tracking, it uses VoidAR solution: https://www.voidar.net/downloads_E.php <br />
You will find in the project /VOIDAR_v1.0_Beta4_Package to use it <br />

You can test GARnie app firstly to see if this solution is compatible with you smartphone.  Normally the compatibility is better than with Arcore Library <br />
For example it works with my Lenovo Phab 2 Pro(published in 2016) which used Tango but is not compatible with Arcore solution

About permission, if you want to do video capture: you need to add storage and microphone permission in option of the app on your smartphone <br />

---------------

And if you bought Trilib plugin and import in the project, you can then import /Packages/import Trilib_1_9_GARnie_Public to use Trilib with GARnie project <br />
To do that, you just need to add in GARnie scene "TrilibCtrl" prefab which is in /TrilibGARnie_Package/Prefabs <br />
Finally you need to add 2 tag name manually on the project: "fileName" and "texture" <br />

You can now play on editor or Android <br />
