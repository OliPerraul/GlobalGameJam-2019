using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatme : MonoBehaviour
{
    [SerializeField]
    CompleteProject.CameraFollow foolow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //var targetObj : GameObject;
    float speed  = 5;
 


    // Update is called once per frame
    void Update()
    {
        if (foolow != null && foolow.target != null)
        {
            Vector3 p = Vector3.zero;

            if (foolow.target2 != null)
            {
                p = (foolow.target.transform.position + foolow.target2.transform.position) / 2;

            }
            else
            {
                p = foolow.target.transform.position;
            }


            var targetRotation = Quaternion.LookRotation(p - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);






        }
     }
}
