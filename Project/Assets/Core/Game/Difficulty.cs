using UnityEngine;
using System.Collections;

namespace Core
{
    public class Difficulty : MonoBehaviour
    {
        public static Difficulty Instance;


        [SerializeField]
        public float BuyStatusIncrementIncr;


        [SerializeField]
        public float BuyStatusCompleteIncr;


        [SerializeField]
        public float StartBuyStatusIncr;


        [SerializeField]
        public float MaxHPIncr;

        [SerializeField]
        public float MinHPIncr;

        public void Awake()
        {
            Instance = this;
        }



    }

}
