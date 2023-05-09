using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchAbility : MonoBehaviour
{

    public Transform hand;
    public string pulling;
    public float modifier = 1.0f;
    Vector3 pullForce;
    public Transform heldObject;
    public float positionDistanceThreshold;
    public float velocityDistanceThreshold;
    public float maxVelocity;
    public float throwVelocity;

    void Update()
    {

        // I want to create a raycast that will allow me to pull object towards me 
        // a function that will allow one to be able to click the mouse button and grab an object

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag.Equals(pulling))
                {
                    StartCoroutine(PullObject(hit.transform));
                }
            }
        }

        /*
            If the player clicks the right mouse button do the following:
            1) make it's parent be nothing
            2) Remove all physics constraints
            3) Set its velocity to be the forward vector of the camera * the throw velocity
            4) set the heldObject variable to null
        */
        if (Input.GetMouseButtonDown(1))
        {
            if (heldObject != null)
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                heldObject.GetComponent<Rigidbody>().velocity = transform.forward * throwVelocity;
                heldObject = null;
            }
        }
    }

    IEnumerator PullObject(Transform t)
    {
        Rigidbody r = t.GetComponent<Rigidbody>();
        while (true)
        {

            //  If the player right-clicks, stop pulling
            if (Input.GetMouseButtonDown(1))
            {
                break;
            }
            float distanceToHand = Vector3.Distance(t.position, hand.position);
            /*
                If the object is withihn the distance threshold, consider it pulled all the way and:
                1) Set the object's position to the hand position
                2) make it's parent be the hand object
                3) Constrain its movement, but not rotation
                4) Set its velocity to be the forward vector of the camera * the throw velocity
                5) Break out of the coroutine
            */
            if (distanceToHand < positionDistanceThreshold)
            {
                t.position = hand.position;
                t.parent = hand;
                r.constraints = RigidbodyConstraints.FreezePosition;
                heldObject = t;
                break;
            }

            //  Calculate the pull direction vector
            Vector3 pullDirection = hand.position - t.position;

            //  Normalize it and multiply by the force modifier
            pullForce = pullDirection.normalized * modifier;

            /*
                Check if the velocity magnitude of the object is less than the maximum velocity
                and
                check if the distance to hand is greater than the distance threshold
            */
            if (r.velocity.magnitude < maxVelocity && distanceToHand > velocityDistanceThreshold)
            {

                //  Add force that takes the object's mass into account
                r.AddForce(pullForce, ForceMode.Force);
            }
            else
            {

                // Set a constant velocity to the object
                r.velocity = pullDirection.normalized * maxVelocity;
            }

            yield return null;
        }
    }
}
