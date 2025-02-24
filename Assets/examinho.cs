using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class examinho : MonoBehaviour
{
    [SerializeField] float limitrange;
    [SerializeField] float speed;
    Vector3 rawirection, direction;
    float rawDistanceDirection;
    // Start is called before the first frame update
    //transform.up direction
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        direction = Vector3.up;
        gameObject.transform.position += direction * speed * Time.deltaTime;
        if (direction.x != 0 && direction.y != 0) 
        {
            float magnitude = Mathf.Sqrt((direction.x*direction.x) + (direction.y * direction.y));
            direction = direction / magnitude;
            
        }
        //if 
    }
}
