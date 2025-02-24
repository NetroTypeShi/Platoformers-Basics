using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exam : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.up = new Vector3(1, 1, 0);  
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
            
            
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
            
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
            
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
           
            
        }
        if(direction.x !=0 && direction.y != 0) { direction.Normalize(); }
        
        gameObject.transform.position += direction * Time.deltaTime * speed;



    }
}
