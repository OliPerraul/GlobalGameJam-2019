using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyme : MonoBehaviour
{
    [SerializeField]
    float limit = 10f;

    float time = 0f;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time <= limit)
        {
            Destroy(gameObject);
        }
        
    }
}
