using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("hit something");
        if (other.tag == "Cart")
        {
            Debug.Log("Entered Checkout Zone");
            double cost = 0;

            Transform[] children = gameObject.transform.Find("Cart_Items").transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                cost += child.GetComponent < GetPrice> ().getPrice();
                Debug.Log(child.name + " ---- " + cost);
            }

            Debug.Log("total cost: " + cost);
        }
    }
}
