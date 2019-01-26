using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{

    public class Level : MonoBehaviour
    {
        [SerializeField]
        public GameObject SpawnPoint;

        [SerializeField]
        private GameObject Upper;
        [SerializeField]
        private GameObject Bottom;
        [SerializeField]
        private GameObject Stairs;

        [SerializeField]
        private Player player;


        private float full = 1f;
        private float none = 0f;

        [SerializeField]
        private float alphaoffset = 1.8f;

        [SerializeField]
        private float denum = 4;

        private MeshRenderer rendUpper;

        public static Level Instance;



        public void Start()
        {
            rendUpper = Upper.GetComponent<MeshRenderer>();
        }

        public void Awake()
        {
            Instance = this;
        }
        
        public void Update()
        {
            //if(player.transform.position.y)
            AdjustTranspa();
        }


        public void AdjustTranspa()
        {
            float f = ((player.transform.position.y - alphaoffset) / denum);

            float alpha = Mathf.Lerp(none, full, f);
            Color c = rendUpper.material.color;
            rendUpper.material.color = new Color(c.r, c.g, c.b, alpha);
           // Debug.Log(rendUpper.material.color);
        }

    }

}
