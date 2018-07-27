using UnityEngine.Networking;
using LitJson;
using System.Collections;
using System.IO;

namespace GameFramework
{
    public class HotUpdateSolution
    {
        public virtual void OnStart(JsonData _data) { }

        protected UnityWebRequest _request = null;

        protected Dictionary<string, string> serverConfigs;
        protected string huUrl;
        protected string localVersion = string.Empty;
        protected Dictionary<string, AssetBundleVersion> localRecord = new Dictionary<string, AssetBundleVersion>();
        protected Dictionary<string, AssetBundleVersion> serverRecord = new Dictionary<string, AssetBundleVersion>();

        public HotUpdateSolution ()
        {
            localVersion = Utility.MergeString(ProjectDatas.PATH_CACHE_PERSISTENT + ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE);
        }

        /// <summary>
        /// 构建版本记录信息
        /// </summary>
        /// <param name="_infos">读取的json字符串</param>
        protected Dictionary<string, AssetBundleVersion> BuildDownloadInfo(string _infos)
        {
            Dictionary<string, AssetBundleVersion> _dic = new Dictionary<string, AssetBundleVersion>();
            if (string.IsNullOrEmpty(_infos))
            {
                return _dic;
            }
            AssetBundleVersion[] _list = JsonMapper.ToObject<AssetBundleVersion[]>(_infos);

            for (int i = 0; i < _list.Length; i++)
            {
                _dic.Add(_list[i].n, _list[i]);
            }
            return _dic;
        }
        /// <summary>
        /// 当结束
        /// </summary>
        protected virtual void OnEnd()
        {
            ResourceModule.instance.Refresh();
            MessageModule.instance.Recevive(string.Empty, MessageType.OnTotalHotupdateComplete, string.Empty);
        }
        /// <summary>
        /// 当完成了步骤
        /// </summary>
		protected void OnStep(float _f)
        {
            MessageModule.instance.Recevive(string.Empty, MessageType.OnHotupdateStep, _f.ToString());
        }
        /// <summary>
        /// 下载版本控制文件
        /// </summary>
        protected IEnumerator DownloadVersionFile()
        {
            string _url = huUrl + ProjectDatas.PATH_PLATFORM_NODE + "/" + ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE;
            _request = UnityWebRequest.Get(_url);
            yield return _request.Send();
            //请求错误，放弃本次热更新
            if (_request.isError)
            {
                Utility.Log("<color=red>", _request.error, "</color>");
                OnEnd();
            }
            else
            {
                if (_request.responseCode == 200 || _request.responseCode == 0)
                {
                    serverRecord = BuildDownloadInfo(_request.downloadHandler.text);
                }
                //下载错误，放弃本次热更新
                else
                {
                    Utility.Log("<color=red>", _url, "  request.error = ", _request.responseCode, "</color>");
                    OnEnd();
                }
            }
        }
        /// <summary>
        /// 开始下载文件列表
        /// </summary>
		protected IEnumerator StartDownload(List<AssetBundleVersion> _needDownload)
        {
            for (int i = 0; i < _needDownload.Count; i++)
            {
                string _furl = Utility.MergeString(huUrl, ProjectDatas.PATH_PLATFORM_NODE, "/", _needDownload[i].n);
                _request = UnityWebRequest.Get(_furl);
                yield return _request.Send();
                if (_request.isError)
                {
                    Utility.LogError(_request.error);
                    //全局热更必须保证成功，一旦失败，提示并退出。
                    MessageModule.instance.Recevive(string.Empty, MessageType.OnHotupdateFailed, "3");
                    yield break;
                }
                else
                {
                    if (_request.responseCode == 200 || _request.responseCode == 0)
                    {
                        UpdateOneFile(_needDownload[i].n, _request.downloadHandler.data);
                        localRecord.Add(_needDownload[i].n, _needDownload[i]);
                        OnStep(i * 1f / _needDownload.Count);
                    }
                    else
                    {
                        Utility.LogError(_request.responseCode);
                        //全局热更必须保证成功，一旦失败，提示并退出。
                        MessageModule.instance.Recevive(string.Empty, MessageType.OnHotupdateFailed, "3");
                        yield break;
                    }
                }
            }

            //更新本地记录
            UpdateLocalVersion();
            OnEnd();
        }
        protected void UpdateOneFile(string _info, byte[] _bytes)
        {
            string _path = Utility.MergeString(ProjectDatas.PATH_CACHE_PERSISTENT, _info);
            string _dir = Path.GetDirectoryName(_path);
            if (!Directory.Exists(_dir))
            {
                Directory.CreateDirectory(_dir);
            }
            File.WriteAllBytes(_path, _bytes);
        }
        protected void UpdateLocalVersion()
        {
            List<AssetBundleVersion> _list = localRecord.ToList();
            string _json = JsonMapper.ToJson(_list);
            File.WriteAllText(localVersion, _json);
        }
    }
}