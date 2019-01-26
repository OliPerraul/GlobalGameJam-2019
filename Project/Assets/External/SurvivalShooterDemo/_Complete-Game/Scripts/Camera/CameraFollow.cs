using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;            // The position that that camera will be following.

        public Transform target2;

        public float smoothing = 5f;        // The speed with which the camera will be following.


        Vector3 offset;                     // The initial offset from the target.

        public static CameraFollow Instance;

        private void Awake()
        {
            Instance = this;
        }

        void Start ()
        {

            DoStart();
        }


        public void DoStart()
        {

            if (target == null)
                return;

            // Calculate the initial offset.
            offset = transform.position - target.position;
        }



        void FixedUpdate ()
        {
            if (target == null)
                return;

            if (target2 == null)
            {

                // Create a postion the camera is aiming for based on the offset from the target.
                Vector3 targetCamPos = target.position + offset;

                // Smoothly interpolate between the camera's current position and it's target position.
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

            }
            else // multi-player
            {
                // Create a postion the camera is aiming for based on the offset from the target.
                Vector3 targetCamPos = ((target.position + offset) + (target2.position + offset))/2;

                // Smoothly interpolate between the camera's current position and it's target position.
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            }




        }
    }
}