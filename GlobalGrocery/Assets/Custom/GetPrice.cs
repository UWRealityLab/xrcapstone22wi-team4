using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPrice : MonoBehaviour
{
    // Start is called before the first frame update
    public double cost;
    void Start()
    {
        GameObject pricingManager = GameObject.Find("PricingManager");
        cost = pricingManager.GetComponent<PricingManager>().getPrice(gameObject.name);
    }

    public double getPrice()
    {
        return cost;
    }
}
