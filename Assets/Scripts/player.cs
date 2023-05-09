using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 5;
    private float moveHorizontal;
    private float moveFront;



    // Update is called once per frame
    void Update()
    {
        // general player movement
        // this allows me to move left and right 
        moveHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * moveHorizontal);


        // this allows me to move front and back
        moveFront = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * moveFront);
    }
}
