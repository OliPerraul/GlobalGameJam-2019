using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


namespace Core
{

    public class Level : MonoBehaviour
    {
        public Collider Bounds;

        public GameObject Entrance;

        public GameObject Exit;

        public GameObject CollectibleSnaps;
        [HideInInspector]
        public Vector3[] CollectibleSnapsArray;

        public GameObject UpperSnapPoints;
        [HideInInspector]
        public Vector3[] UpperSnapPointsArray;

        public GameObject DownSnapPoints;
        [HideInInspector]
        public Vector3[] DownSnapPointsArray;


        [SerializeField]
        public GameObject SpawnPoint;

        [SerializeField]
        public GameObject SpawnPointP1;

        [SerializeField]
        public GameObject SpawnPointP2;

        [SerializeField]
        public GameObject[] SpawnVisitors;



        [SerializeField]
        private GameObject Upper;
        [SerializeField]
        private GameObject Bottom;
        [SerializeField]
        private GameObject Stairs;

        [SerializeField]
        public Player player;

        [SerializeField]
        public Player player2;

        private float full = 1f;
        private float none = 0f;

        [SerializeField]
        private float alphaoffset = 1.8f;

        [SerializeField]
        private float denum = 4;

        public MeshRenderer rendUpper;

        [Tooltip("Furnitures mesh renderer")]
        public MeshRenderer[] furnitures;

        public static Level Instance;


        public NavMeshSurface NMSurf;

        public void Awake()
        {
            Instance = this;

            CollectibleSnapsArray = new Vector3[CollectibleSnaps.transform.childCount];
            int i = 0;
            foreach (Transform child in CollectibleSnaps.transform)
            {
                CollectibleSnapsArray[i] = child.position; i++;
                //child is your child transform
            }


            UpperSnapPointsArray = new Vector3[UpperSnapPoints.transform.childCount];
            i = 0;
            foreach (Transform child in UpperSnapPoints.transform)
            {
                UpperSnapPointsArray[i] = child.position; i++;
                //child is your child transform
            }

            DownSnapPointsArray = new Vector3[DownSnapPoints.transform.childCount];
            i = 0;
            foreach (Transform child in DownSnapPoints.transform)
            {
                DownSnapPointsArray[i] = child.position; i++;
                //child is your child transform
            }

        }
        
        public void Update()
        {
            if (player != null)
            {

                //if(player.transform.position.y)
                AdjustTranspa();
            }
        }


        public void AdjustTranspa()
        {
            float f = ((player.transform.position.y - alphaoffset) / denum);

            float alpha = Mathf.Lerp(none, full, f);
            Color c = rendUpper.material.color;
            rendUpper.material.color = new Color(c.r, c.g, c.b, alpha);
            
            
            foreach (MeshRenderer furnitureRenderer in furnitures)
            {
                Material[] materials = furnitureRenderer.materials;
                Debug.Log("Materials " + materials.Length); 
                foreach (Material mateirial in materials)
                {
                    c = mateirial.color;
                    mateirial.color = new Color(c.r, c.g, c.b, alpha);
                }
            }



            if (player2 == null) return;

            if ((Mathf.Approximately((player2.transform.position.y - alphaoffset), 0)))
            {
                f = ((player.transform.position.y - alphaoffset) / denum);

                alpha = Mathf.Lerp(none, full, f);
                c = rendUpper.material.color;
                rendUpper.material.color = new Color(c.r, c.g, c.b, alpha);
                // Debug.Log(rendUpper.material.color);

            }


        }

    }

}
