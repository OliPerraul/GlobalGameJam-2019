using UnityEngine;
using UnityEditor;
using System;

namespace Core.FSM
{
    public abstract class Resource : ScriptableObject
    {
        [SerializeField]
        public bool IsDebugMessageEnabled = false;
        virtual public int Id { get { return -1; } }
        public abstract State Create(object[] context);
    }

}
