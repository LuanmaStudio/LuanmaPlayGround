using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// 全局物体保存
    /// </summary>
    public class GlobalGameObject : Singleton<GlobalGameObject>
    {
        private Dictionary<string, GameObject> _objects = new Dictionary<string, GameObject>();

        public GameObject Get(string name)
        {
            if (_objects.ContainsKey(name))
            {
                return _objects[name];
            }
            else
            {
                _objects[name] = GameObject.Find(name);
                return _objects[name];
            }
        }
    }
}