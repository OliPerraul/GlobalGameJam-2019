using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Collectibles
{

    public class Collectible : MonoBehaviour
    {
        [SerializeField]
        public Resource Resource;

        [SerializeField]
        public float limitIdle = 2f;

        [SerializeField]
        private float rotateSpeed;

        [SerializeField]
        private GameObject circle;

        // Speed in units per sec.
        [SerializeField]
        private float chaseSpeed;

        private float timeIdle = 0f;
        private Player character = null;

        [SerializeField]
        private GameObject graphic;

        public void Update()
        {
            if (graphic != null)
                graphic.transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);

            if (timeIdle >= limitIdle)
            {
                if (character != null)
                {
                    // The step size is equal to speed times frame time.
                    float step = chaseSpeed * Time.deltaTime;

                    // Move our position a step closer to the target.
                    transform.position = Vector3.MoveTowards(transform.position, character.transform.position, step);
                    if (Utils.Vectors.CloseEnough(transform.position, character.transform.position))
                    {
                        character.Collect(Resource); //TODO
                        Collect();
                    }
                }
            }
            else
            {
                timeIdle += Time.deltaTime;
            }

        }

        public void OnTriggerEnter(Collider other)
        {
            if (character == null)//only try to get the gem if null target
            {
                character = other.GetComponent<Player>();
                if (character != null)
                {
                    //Do not collect if already collected
                    if (character.Collectible != null)
                    {
                        character = null;
                        return;
                    }

                    if (circle != null) Destroy(circle.gameObject);
                }
            }
        }


        public void Collect()
        {
            Level.Instance.collectablesMesh.Remove(gameObject);
            Destroy(gameObject);
            
        }

    }




}
