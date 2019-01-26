using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

namespace Core
{

    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int id = 0;


        [SerializeField]
        float speed = 5; // units per second

        [SerializeField]
        float turnSpeed = 90; // degrees per second

        [SerializeField]
        float jumpSpeed = 8;

        [SerializeField]
        float gravity = 9.8f;

        [SerializeField]
        float vSpeed = 0; // current vertical velocity


        public Collectibles.Resource Collectible;

        public Interactables.Interactable Interactable = null;

        string input_hor = "Horizontal";
        //string input_hor = "Horizontal";
        string intpu_Vertical = "Vertical";
        string intpu_fire = "Fire1";


        public void Start()
        {
            if (id == 0)
            {
                input_hor = "P1_Horizontal";
                intpu_Vertical = "P1_Vertical";
                intpu_fire = "P1_Fire1";
            }
            else
            {
                input_hor = "P2_Horizontal";
                intpu_Vertical = "P2_Vertical";
                intpu_fire = "P2_Fire1";

            }
        

        }


        public void Update()
        {

            Debug.Log(input_hor);

            Vector3 vel = new Vector3(Input.GetAxis(input_hor) * speed, 0, Input.GetAxis(intpu_Vertical) * speed);

            if (vel.magnitude > .5f)
            {
                transform.rotation = Quaternion.LookRotation(vel);
            }

            var controller = GetComponent<CharacterController>();//CharacterController);
            if (controller.isGrounded)
            {
                vSpeed = 0; // grounded character has vSpeed = 0...
                if (Input.GetKeyDown("space"))
                { // unless it jumps:
                  //NO JUMP lol
                  //vSpeed = jumpSpeed;
                }
            }
            // apply gravity acceleration to vertical speed:
            vSpeed -= gravity * Time.deltaTime;
            vel.y = vSpeed; // include vertical speed in vel
                            // convert vel to displacement and Move the character:
            controller.Move(vel * Time.deltaTime);

            if (Input.GetButtonDown(intpu_fire))
            {
                if (Interactable != null)
                {
                    Interactable.Interact();
                }
                else
                if (Collectible != null)
                {
                    LayTrap();
                }
            }

        }

        public void Collect(Collectibles.Resource res)
        {
            Collectible = res;
        }

        public void LayTrap()
        {
            Instantiate(Collectible.Trap, transform.position+Vector3.up*.5f, Quaternion.identity);
            Collectible = null; //no more trap to lay
        }

        public void Interact()
        {
            
        }

    }
}