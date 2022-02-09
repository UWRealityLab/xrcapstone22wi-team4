using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCartBehavior : MonoBehaviour
{
    /*private Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cart Collider")
        {
            Debug.Log("Entered Cart");
            Rigidbody rg = GetComponent<Rigidbody>();
            if (!rg.isKinematic)
            {
                StartCoroutine("cartDelayEnter", other);
            }
            else
            {
                Debug.Log("Changed isKinematic to false");
                rg.isKinematic = false;
                rg.useGravity = true;
                transform.parent = null;
                Debug.Log(transform.parent);
            }

        }
    }

    IEnumerator cartDelayEnter(Collider other)
    {
        Debug.Log("entered");
        yield return new WaitForSeconds(0.5f);
        Rigidbody rg = GetComponent<Rigidbody>();
        Debug.Log("Changed isKinematic to true");
        rg.isKinematic = true;
        rg.velocity = Vector3.zero;
        rg.centerOfMass = Vector3.zero;
        rg.inertiaTensorRotation = Quaternion.identity;
        Vector3 originalScale = transform.localScale;
        Transform cart_items = other.transform.parent.transform.Find("Cart_Items").transform;
        transform.SetParent(cart_items);
        transform.localScale = originalScale;

        /*GameObject cart = GameObject.Find("Cart_nabruh");
        Rigidbody cart_rg = cart.GetComponent<Rigidbody>();
        cart_rg.velocity = Vector3.zero;*/
    }
}