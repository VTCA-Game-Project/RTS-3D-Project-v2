using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class AssetUtils
    {
        private static readonly AssetUtils instance = new AssetUtils();
        private Dictionary<int, Object> assets;

        private AssetUtils()
        {
            assets = new Dictionary<int, Object>();
            Load(@"Prefabs", false);
        }

        public void Load(string path, bool clearPreload)
        {
            if (clearPreload) assets.Clear();
            Object[] allAssets = Resources.LoadAll(path);
            for (int i = 0; i < allAssets.Length; i++)
            {
                assets.Add(allAssets[i].name.GetHashCode(), allAssets[i]);
            }
        }

        public Object GetAsset(string name)
        {
            Object asset = null;
            assets.TryGetValue(name.GetHashCode(), out asset);
#if UNITY_EDITOR
            if (asset == null) Debug.Log("Not found asset");
#endif
            return asset;

        }

        public static AssetUtils Instance
        {
            get { return instance; }
        }
    }
}