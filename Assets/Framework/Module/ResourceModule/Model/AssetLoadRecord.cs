using UnityEngine;
using System.Collections;

namespace GameFramework
{
    public class AssetLoadRecord : INeedDestroy, IName
    {
        public string name { get; private set; }
        public AssetLoadRecord (string _name)
        {
            name = _name.ToLower();
        }

        private List<string> resUsed = new List<string>();

        public void Destroy()
        {
            for (int i = 0; i < resUsed.Count; i++)
            {
                ResourceModule.instance.UnloadAsset(name, resUsed[i]);
            }
        }

        public GameObject LoadGameObject(string _resName)
        {
            return LoadResource<GameObject>(_resName);
        }
        public GameObject LoadInstantiateGameObject(string _resName)
        {
			GameObject _go = LoadResource<GameObject> (_resName);
			if (_go == null) return _go;
            return GameObject.Instantiate(LoadResource<GameObject>(_resName));
        }
        public Sprite LoadSprite(string _resName)
        {
            return LoadResource<Sprite>(_resName);
        }
        public Texture LoadTexture(string _resName)
        {
            return LoadResource<Texture>(_resName);
        }
        public Texture2D LoadTexture2D(string _resName)
        {
            return LoadResource<Texture2D>(_resName);
        }
        public string LoadText(string _resName)
        {
            TextAsset _t = LoadResource<TextAsset>(_resName);
            return _t == null ? string.Empty : _t.text;
        }
        public Material LoadMaterial(string _resName)
        {
            return LoadResource<Material>(_resName);
        }
        public AudioClip LoadAudioClip(string _resName)
        {
            return LoadResource<AudioClip>(_resName);
        }
        public T LoadResource<T>(string _resName) where T : Object
        {
            _resName = _resName.ToLower();
            resUsed.Add(_resName);
            return ResourceModule.instance.LoadAsset<T>(name, _resName);
        }

        public LuaInterface.LuaByteBuffer LoadLuaFileBytes(string _resName)
        {
            byte[] _bs = null;
#if UNITY_EDITOR
            _bs = ResourceModule.instance.LoadAssetBytes<TextAsset>(name, _resName);
#else
            TextAsset _t = LoadResource<TextAsset>(_resName);
            if (_t != null) _bs = _t.bytes;
#endif
            return new LuaInterface.LuaByteBuffer(_bs);
        }
    }
}