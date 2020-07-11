using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    Vector3 startPosition;
    public GameObject cameraOriginTransform; 
    float speed;
    private float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }
 
    // Update is called once per frame
    void Update()
    { 
        speed = GetComponentInParent<Rigidbody>().velocity.magnitude;
         
        transform.position = Vector3.Lerp(cameraOriginTransform.transform.position  , cameraOriginTransform.transform.position - transform.forward * speed /5 , 1);

        horizontal = Input.GetAxis("Horizontal");
         
         

         




    }
}
