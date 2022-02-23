using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayItemPrice : MonoBehaviour
{
    private PricingManager pricingManager;

    // Start is called before the first frame update
    void Start()
    {
        pricingManager = GameObject.Find("PricingManager").GetComponent<PricingManager>();
    }

    // Update is called once per frame
    public void onDisplay(string name)
    {
        double price = pricingManager.getPrice(name);
        TextMeshPro text = gameObject.transform.GetComponent<TextMeshPro>();
        text.text = name + "\n" + "Price: " + price;
        gameObject.transform.GetComponent<MeshRenderer>().enabled = true;
    }

    public void offDisplay()
    {
        gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
    }
}
