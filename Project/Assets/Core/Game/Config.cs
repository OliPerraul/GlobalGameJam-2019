using UnityEngine;
using System.Collections;

namespace Core
{
    [System.Serializable]
    public struct ConfigValues
    {
        [SerializeField]
        public float VisitTimeMin;

        [SerializeField]
        public float VisitTimeMax;

        [SerializeField]
        public float HousePrice;

        [SerializeField]
        public float StartBuyStatus;

        [SerializeField]
        public float BuyStatusIncrement;

        [SerializeField]
        public float BuyStatusComplete;


        [SerializeField]
        public float StartupTime;


        [SerializeField]
        public float NumVisitorMin;

        [SerializeField]
        public float NumVisitorMax;


        [SerializeField]
        public float EndTime;

        [SerializeField]
        public float MaxHP;

        [SerializeField]
        public float MinHP;


        [SerializeField]
        public float TrapSpawnFreqMax;

        [SerializeField]
        public float TrapSpawnFreqMin;

    }
       
    public class Config : MonoBehaviour
    {
        public static Config Instance;

        public void Awake()
        {
            Instance = this;
        }

        [SerializeField]
        public ConfigValues StartValues;



    }

}
