using UnityEngine;
using System.Collections;

namespace Core
{
    public class Library : MonoBehaviour
    {
        public static Library Instance;

        public void Awake()
        {
            Instance = this;
        }

        // CHARAS

        [SerializeField]
        public GameObject REAgent;


        [SerializeField]
        public GameObject Player1;

        [SerializeField]
        public GameObject Player2;


        [SerializeField]
        public GameObject Visitor;

        [SerializeField]
        public GameObject TextWaveClear;


        [SerializeField]
        public GameObject Collect1;


        [SerializeField]
        public GameObject Collect2;

        [SerializeField]
        public GameObject Collect3;

        [SerializeField]
        public GameObject Collect4;

        // TRAPs

        [SerializeField]
        public GameObject Trap1;

        [SerializeField]
        public GameObject Trap2;

        [SerializeField]
        public GameObject Trap3;


        [SerializeField]
        public Sprite spriteBroked;


    }

}
