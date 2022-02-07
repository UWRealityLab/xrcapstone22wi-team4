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
        if (other.tag == "Cart")
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
                Debug.Log("here 1");
                rg.useGravity = true;
                Debug.Log("here 2");
                transform.parent = null;
                Debug.Log("here 3");
            }

        }
    }

    IEnumerator cartDelayEnter(Collider other)
    {
        yield return new WaitForSeconds(2);
        Rigidbody rg = GetComponent<Rigidbody>();
        Debug.Log("Changed isKinematic to true");
        rg.velocity = Vector3.zero;
        rg.isKinematic = true;
        rg.velocity = Vector3.zero;
        Vector3 originalScale = transform.localScale;
        transform.SetParent(other.transform.parent.transform);
        transform.localScale = originalScale;

        /*GameObject cart = GameObject.Find("Cart_nabruh");
        Rigidbody cart_rg = cart.GetComponent<Rigidbody>();
        cart_rg.velocity = Vector3.zero;*/
    }
}