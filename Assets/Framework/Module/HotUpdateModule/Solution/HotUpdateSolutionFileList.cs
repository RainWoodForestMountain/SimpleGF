using UnityEngine.Networking;
using LitJson;
using System.Collections;
using System.IO;

namespace GameFramework
{
    public class HotUpdateSolutionFileList : HotUpdateSolution
    {
        private List<string> needDown = new List<string>();

        public HotUpdateSolutionFileList () : base (){}

        public override void OnStart(JsonData _data)
        {
            huUrl = RunningTimeData.GetRunningData(CommonKey.URL_HOTUPDATE);
            string _filesjson = _data["files"].Value;
            string[] _files = JsonMapper.ToObject<string[]>(_filesjson);
            if (_data["emergent"].BoolValue)
            {
                List<string> _temp = new List<string>();
                _temp.AddRange(_files);
                _temp.AddRange(needDown);
                needDown = _temp;
            }
            else
            {
                needDown.AddRange(_files);
            }
            
            TimeModule.instance.RunIEnumerator(OnDownloadFileList());
        }

        private IEnumerator OnDownloadFileList()
        {
            yield return DownloadVersionFile();

            List<AssetBundleVersion> _dfs = new List<AssetBundleVersion>();
            for (int i = 0; i < needDown.Count; i++)
            {
                if (serverRecord.ContainsKey(needDown[i]))
                {
                    //如果本地存在，判断是否需要更新
                    if (localRecord.ContainsKey(needDown[i]) && localRecord[needDown[i]].NeedDownloadThan(serverRecord[needDown[i]]))
                    {
                        _dfs.Add(serverRecord[needDown[i]]);
                    }
                    else
                    {
                        _dfs.Add(serverRecord[needDown[i]]);
                    }
                }
            }

            yield return StartDownload(_dfs);
        }
    }
}