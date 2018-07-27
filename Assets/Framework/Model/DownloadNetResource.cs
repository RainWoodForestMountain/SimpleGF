using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
    internal abstract class DownloadNetResourcePackage
    {
        public string localPath;
        public string servetPath;
        private string dir;

        protected GameObject obj;
        protected string sym;
        private bool force;

        public DownloadNetResourcePackage(GameObject _obj, string _url, bool _force)
        {
            obj = _obj;
            sym = Utility.MakeUnifiedDirectory(_url).Replace(":", "_").Replace("/", "_");
            force = _force;
            servetPath = _url;
            
            dir = Utility.MergeString(ProjectDatas.PATH_CACHE_PERSISTENT, ProjectDatas.PATH_CACHE_NODE);
            localPath = Utility.MergeString(dir, "/", sym);
        }

        public void Saved (byte[] _bs)
        {
            if (!string.IsNullOrEmpty(sym))
            {
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                File.WriteAllBytes(localPath, _bs);
            }
        }
        public virtual bool NeedDownload ()
        {
            if (force) return true;
            if (File.Exists(localPath))
            {
                OnComplete(File.ReadAllBytes(localPath));
                return false;
            }
            else return true;
        }
        public abstract void OnComplete(byte[] _bs);
    }

    internal class DownloadNetResourcePackage_Sprite : DownloadNetResourcePackage
    {
        private static Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();

        public int width;
        public int heigth;

        public DownloadNetResourcePackage_Sprite(GameObject _obj, string _url, int _heigth, int _width, bool _force = false) : base(_obj, _url, _force)
        {
            width = _width;
            heigth = _heigth;
        }

        public override bool NeedDownload()
        {
            if (!string.IsNullOrEmpty(sym) && cache.ContainsKey(sym))
            {
                Set(cache[sym]);
                return false;
            }
            return base.NeedDownload();
        }
        public override void OnComplete(byte[] _bs)
        {
            Texture2D _tex = new Texture2D(width, heigth);
            _tex.LoadImage(_bs);
            Sprite _sp = Sprite.Create(_tex, new Rect(0, 0, _tex.width, _tex.height), new Vector2(0.5f, 0.5f));
            if (!string.IsNullOrEmpty(sym)) cache.Add(sym, _sp);
            Set(_sp);
        }

        private void Set (Sprite _sp)
        {
            if (obj)
            {
                Image _image = obj.GetComponent<Image>();
                SpriteRenderer _sr = obj.GetComponent<SpriteRenderer>();

                if (_image) _image.sprite = _sp;
                if (_sr) _sr.sprite = _sp;
            }
        }
    }

    public class DownloadNetResource
    {
        private static bool isRunning = false;
        private static Queue<DownloadNetResourcePackage> ques = new Queue<DownloadNetResourcePackage>();

        private static void Loaded ()
        {
            if (!isRunning)
            {
                isRunning = true;
                TimeModule.instance.RunIEnumerator(Downloading());
            }
        }

        private static IEnumerator Downloading()
        {
            string _cachePath = Utility.MergeString(ProjectDatas.PATH_CACHE_PERSISTENT, ProjectDatas.PATH_CACHE_NODE, "/");
            while (ques.Count > 0)
            {
                DownloadNetResourcePackage _one = ques.Dequeue();
                if (_one.NeedDownload())
                {
                    UnityWebRequest _request = UnityWebRequest.Get(_one.servetPath);
                    yield return _request.Send();
                    //请求错误，放弃本次热更新
                    if (_request.isError)
                    {
                        Utility.LogError("资源", _one.servetPath, _request.error);
                    }
                    else
                    {
                        if (_request.responseCode == 200 || _request.responseCode == 0)
                        {
                            _one.Saved(_request.downloadHandler.data);
                            _one.OnComplete(_request.downloadHandler.data);
                        }
                        else
                        {
                            Utility.LogError("资源", _one.servetPath, "下载错误：", _request.responseCode);
                        }
                    }
                }
                else yield return new WaitForEndOfFrame();
            }

            isRunning = false;
            yield break;
        }

        public static void LoadedSprite (GameObject _obj, string _url, int _heigth, int _width, bool _force = false)
        {
            ques.Enqueue(new DownloadNetResourcePackage_Sprite(_obj, _url, _heigth, _width, _force));
            Loaded();
        }
    }
}