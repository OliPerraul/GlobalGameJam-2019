using UnityEngine;
using System.Collections;

namespace Core.Collectibles
{
    public enum Id
    {
        Trap1,
        Trap2,
        
    }

    public class Resource : ScriptableObject
    {
        [SerializeField]
        public GameObject Trap;
    }

}
