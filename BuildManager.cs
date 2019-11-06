using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildManager
{
    //[MenuItem("Tools/Build Scene")]
    //private static void BuildScene()
    //{
    //    //清空一下缓存  
    //    Caching.CleanCache();

    //    //获得用户选择的路径的方法，可以打开保存面板（推荐）
    //    string Path = EditorUtility.SaveFilePanel("保存资源", "SS", "" + "SampleScene", "unity3d");

    //    //另一种获得用户选择的路径，默认把打包后的文件放在Assets目录下
    //    //string Path = Application.dataPath + "/MyScene.unity3d";

    //    //选择的要保存的对象 
    //    string[] levels = { "Assets/Scenes/SampleScene.unity" };
    //    //BuildPipeline.BuildStreamedSceneAssetBundle(levels, Path, BuildTarget.StandaloneWindows64, BuildOptions.BuildAdditionalStreamedScenes);
    //    //打包场景  
    //    BuildPipeline.BuildPlayer(levels, Path, EditorUserBuildSettings.activeBuildTarget, BuildOptions.BuildAdditionalStreamedScenes);

    //    // 刷新，可以直接在Unity工程中看见打包后的文件
    //    AssetDatabase.Refresh();
    //}


    [MenuItem("Tools/All Target Build Asset Bundles")]
    private static void AllTargetBuildAssetBundle()
    {
        WindowsBuildAssetBundle();
        AndroidBuildAssetBundle();
        IosBuildAssetBundle();
    }

    [MenuItem("Tools/Windows Build Asset Bundles")]
    private static void WindowsBuildAssetBundle()
    {
        string windowsTargetPath = Application.streamingAssetsPath + "/windows";
        //windowsTargetPath = windowsTargetPath.Replace("_art", "");
        //windowsTargetPath = Application.streamingAssetsPath + "/android";
        if (!Directory.Exists(windowsTargetPath))
        {
            Directory.CreateDirectory(windowsTargetPath);
        }

        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression;
        BuildPipeline.BuildAssetBundles(windowsTargetPath, options, BuildTarget.StandaloneWindows64);
        //BuildPipeline.BuildAssetBundles(windowsTargetPath, options, EditorUserBuildSettings.activeBuildTarget);
        //BuildPipeline.BuildAssetBundles(targetPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();
        Debug.Log("打包完成");
    }

    [MenuItem("Tools/Android Build Asset Bundles")]
    private static void AndroidBuildAssetBundle()
    {
        string androidTargetPath = Application.streamingAssetsPath + "/android";

        if (!Directory.Exists(androidTargetPath))
        {
            Directory.CreateDirectory(androidTargetPath);
        }

        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression;
        BuildPipeline.BuildAssetBundles(androidTargetPath, options, BuildTarget.Android);
        //BuildPipeline.BuildAssetBundles(targetPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();
        Debug.Log("打包完成");
    }

    [MenuItem("Tools/Ios Build Asset Bundles")]
    private static void IosBuildAssetBundle()
    {
        string iosTargetPath = Application.streamingAssetsPath + "/windows";

        if (!Directory.Exists(iosTargetPath))
        {
            Directory.CreateDirectory(iosTargetPath);
        }

        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression;
        BuildPipeline.BuildAssetBundles(iosTargetPath, options, BuildTarget.iOS);
        //BuildPipeline.BuildAssetBundles(targetPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();
        Debug.Log("打包完成");
    }

    [MenuItem("Tools/Set AssetBundle Name")]  //将选定的资源进行统一设置AssetBundle名
    private static void SetBundleName()
    {
        Object[] selects = Selection.objects;
        foreach (Object item in selects)
        {
            string path = AssetDatabase.GetAssetPath(item);
            AssetImporter asset = AssetImporter.GetAtPath(path);
            asset.assetBundleName = item.name;//设置Bundle文件的名称
            asset.assetBundleVariant = "asset";//设置Bundle文件的扩展名
            asset.SaveAndReimport();
        }
        AssetDatabase.Refresh();
        Debug.Log("命名完成");
    }

    [MenuItem("Tools/Clear AssetBundle Name")]
    private static void ClearAssetBundlesName()
    {
        string[] bundleNameArr = AssetDatabase.GetAllAssetBundleNames();
        int length = bundleNameArr.Length;
        //Debug.Log(length);
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = bundleNameArr[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }
        //length = AssetDatabase.GetAllAssetBundleNames().Length;
        //Debug.Log(length);
        Debug.Log("清除命名完成");
    }
}
