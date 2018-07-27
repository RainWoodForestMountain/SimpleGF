using UnityEngine.Networking;
using LitJson;
using System.Collections;
using System.IO;
using UnityEngine;

namespace GameFramework
{
    public class HotUpdateSolutionTotal : HotUpdateSolution
    {
        public HotUpdateSolutionTotal() : base() { }

        /// <summary>
        /// 开始更新
        /// </summary>
		public override void OnStart(JsonData _data)
        {
//#if UNITY_EDITOR
//            base.OnEnd();
//            return;
//#endif
            huUrl = RunningTimeData.GetRunningData(CommonKey.URL_HOTUPDATE);
            if (string.IsNullOrEmpty(huUrl))
            {
                Utility.LogError("没有有效的热更地址，将直接结束热更");
                OnEnd();
                return;
            }
            huUrl += "/";

            TimeModule.instance.RunIEnumerator(DownloadTotalFile());
        }
        /// <summary>
        /// 下载必须的总控文件
        /// </summary>
        private IEnumerator DownloadTotalFile()
        {
            string _url = huUrl + ProjectDatas.NAME_ASSET_BUNDLE_TOTAL_FILE;
            _request = UnityWebRequest.Get(_url);
            yield return _request.Send();
            if (_request.isError)
            {
                Utility.LogError(_request.error);
                //如果总控文件下载失败，不能继续游戏！
                MessageModule.instance.Recevive(string.Empty, MessageType.OnHotupdateFailed, "1");
            }
            else
            {
                //无错误码或者错误码等于200为正确
                if (_request.responseCode == 200 || _request.responseCode == 0)
                {
                    string _text = _request.downloadHandler.text;
                    Utility.Log("总控文件下载：\n", _text);
                    serverConfigs = Utility.DepartDictionaryByLines(_text);
                    CheckVersion();
                }
                else
                {
                    Utility.LogError(_url, "  request.error = ", _request.responseCode);
                    //如果总控文件下载失败，不能继续游戏！
                    MessageModule.instance.Recevive(string.Empty, MessageType.OnHotupdateFailed, "1");
                }
            }
        }
        /// <summary>
        /// 检查更新版本
        /// </summary>
        private void CheckVersion()
        {
            //本地版本和服务器版本
            string _serverVersion = serverConfigs[CommonKey.VERSION_SERVER];
            string _localVersion = RunningTimeData.GetRunningData(CommonKey.VERSION_LOCAL, "0.0.0.0");
            System.Version _sv = new System.Version(_serverVersion);
            System.Version _lv = new System.Version(_localVersion);

            //大版本变更，需要更新整包
            if (_sv.Major > _lv.Major || _sv.Minor > _lv.Minor)
            {
                //给出提示，引导跳转
                MessageModule.instance.Recevive(string.Empty, MessageType.OnHotupdateFailed, "2");
            }
            //小版本变更或者无版本变更，检查热更，保证文件正确性，避免被修改
            else
            {
                TimeModule.instance.RunIEnumerator(OnDownloadVersionFile());
            }
        }
        private IEnumerator OnDownloadVersionFile()
        {
            yield return DownloadVersionFile();
            BuildVersionAndCompare();
        }
        /// <summary>
        /// 开始比较网络和本地记录
        /// </summary>
        private void BuildVersionAndCompare()
        {
            if (!Directory.Exists(ProjectDatas.PATH_CACHE_PERSISTENT))
            {
                Directory.CreateDirectory(ProjectDatas.PATH_CACHE_PERSISTENT);
            }
            //读取本地配置
            string _lc = Utility.LoadPersistentConfigFile(ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE);
            localRecord = BuildDownloadInfo(_lc);
            if (!File.Exists(localVersion))
            {
                File.WriteAllText(localVersion, _lc);
            }

            //下载规则：
            //	1.本地不存在
            //	2-1.本地版本号更低
            //	2-2.md5值有差异
            List<AssetBundleVersion> _needDownload = new List<AssetBundleVersion>();
            foreach (var _kvp in serverRecord)
            {
                if (localRecord.ContainsKey(_kvp.Key))
                {
                    if (localRecord[_kvp.Key].NeedDownloadThan(_kvp.Value))
                    {
                        _needDownload.Add(_kvp.Value);
                    }
                }
                else
                {
                    _needDownload.Add(_kvp.Value);
                }
            }

            TimeModule.instance.RunIEnumerator(StartDownload(_needDownload));
        }
        protected override void OnEnd()
        {
            //更新本地版本号
            string _serverVersion = serverConfigs[CommonKey.VERSION_SERVER];
            RunningTimeData.SetRunningData(CommonKey.VERSION_LOCAL, _serverVersion);
            RunningTimeData.Record();
            base.OnEnd();
        }
    }
}