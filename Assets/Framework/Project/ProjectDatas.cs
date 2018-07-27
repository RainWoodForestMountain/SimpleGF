using UnityEngine;

using System;
using System.IO;
using System.Collections;

namespace GameFramework
{
	public static class ProjectDatas
	{
        /// <summary>
        /// 加密key
        /// </summary>
        public static string CODEKEY = "abcdefghijklmnop12345678";

		public const string LOG_SYMBOL = "[GAME_FRAMEWORK_LOG]: ";
        
        public const string NAME_MANIFESET_FILE = "index";
        public const string NAME_ASSETBUNDLE_EXTENSION = ".gfab";
        public const string NAME_MANIFEST_EXTENSION = ".manifest";
        public const string NAME_RUNNING_TIME_DATA = "runningtime_data.bytes";
        public const string NAME_CHANNEL_FILE = "channel.bytes";
        public const string NAME_ASSET_BUNDLE_INFO_FILE = "name_bundle_kvp.bytes";
        public const string NAME_ASSET_BUNDLE_VERSION_FILE = "version.bytes";
        public const string NAME_ASSET_BUNDLE_TOTAL_FILE = "total.bytes";
        public const string NAME_TEMP_VOICE_FILE = "temp_voice.voi";

        public const string PATH_CONFIG_NODE = "/Config";
        public const string PATH_FRAMEWORK_NODE = "/Framework";
        public const string PATH_ASSETS_NODE = "/Assets";
        public const string PATH_COMMON_NODE = "/Common";
        public const string PATH_CACHE_NODE = "/cache";
        public const string PATH_CACHE_SPRITE = "/sprite";
        public static string PATH_PLATFORM_NODE
        {
            get
            {
#if UNITY_ANDROID
                return "/android";
#elif UNITY_IPHONE
                return "/ios";
#else
                return "/window";
#endif
            }
        }
        public static string PATH_CACHE
        {
            get { return Application.persistentDataPath + "/cache/"; }
        }
        public static string PATH_ASSETS
        {
#if UNITY_EDITOR
            get { return EDITOR_ASSET_SAVE_CURRENT_ROOT; }
#else
            get { return Application.persistentDataPath + "/assets/"; }
#endif
        }
        public static string PATH_CACHE_PERSISTENT { get { return Application.persistentDataPath + "/assets/"; } }
        public static string PATH_CACHE_STREAMING
        {
#if UNITY_EDITOR
            get { return Application.streamingAssetsPath + "/assets/"; }
#elif UNITY_ANDROID
            get { return Application.dataPath + "!assets/assets/"; }
#else
            get { return Application.streamingAssetsPath + "/assets/"; }
#endif
        }

        public static string EDITOR_CODE_MODEL_PATH { get { return Application.dataPath + PATH_FRAMEWORK_NODE + "/Editor/Model/"; } }
		public readonly static string EDITOR_ASSET_SAVE_ROOT = "Assets/AssetsPackages" + PATH_PLATFORM_NODE;
        public readonly static string EDITOR_ASSET_SAVE_CURRENT_ROOT = "Assets/AssetsPackages" + PATH_PLATFORM_NODE + "/Current/";
        public readonly static string EDITOR_ASSET_SAVE_TEMP_ROOT = "Assets/AssetsPackages" + PATH_PLATFORM_NODE + "/Temp/";
        public readonly static string EDITOR_ASSET_MODEL_FILE = "Assets/GameContent/AssetsModelFile.bytes";
		public readonly static string EDITOR_MODULE_ROOT = "Assets/GameContent/Modules/";
        public readonly static string EDITOR_GAME_CONTENT_ROOT = "Assets/GameContent/";
        public readonly static string EDITOR_RESOURCE_ROOT = "Assets/Framework/Resources/";

        public static bool openUIOperate = true;
        public static bool isDebug { get { return Debug.isDebugBuild; } }
	}
}