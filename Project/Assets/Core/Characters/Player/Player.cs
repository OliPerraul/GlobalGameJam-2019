using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

namespace Core
{

    public class Player : MonoBehaviour
    {
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


        public void Update()
        {
            Vector3 vel = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);

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

            // Lay trap
            if (Input.GetButtonDown("Fire1"))
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
            Instantiate(Collectible.Trap, transform.position, Quaternion.identity);
            Collectible = null; //no more trap to lay
        }

        public void Interact()
        {
            
        }

    }
}