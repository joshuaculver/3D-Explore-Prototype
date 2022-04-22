/*
    Used by the player object. When in range of an object which can be interacted it calls
    said objects interaction function. 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    //How far the interactor can be from the interactable
    public float radius = 1.5f;

    //Keeps interactor from activing a new interaction while one is happening
    public bool canInteract = true;

    // Update is called once per frame
    void Update()
    {
        if(canInteract)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                //Stores colliders which overlap from a sphere from the interactor
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

                foreach (Collider hitCollider in hitColliders)
                {
                    //Checks if the interactor is facing the interactable
                    Vector3 direction = hitCollider.transform.position - transform.position;
                    if (Vector3.Dot(transform.forward, direction) > .5f)
                    {
                        //Attempts to call interactables function
                        hitCollider.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }
}
