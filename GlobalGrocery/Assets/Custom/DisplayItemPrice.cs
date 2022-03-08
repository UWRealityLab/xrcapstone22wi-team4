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

    // called when pricing text display is on
    public void onDisplay(string name)
    {
        double price = pricingManager.getPrice(name);
        TextMeshPro text = gameObject.transform.GetComponent<TextMeshPro>();
        text.text = name + "\n" + "Price: " + price;
        gameObject.transform.GetComponent<MeshRenderer>().enabled = true;

        text.transform.SetParent(GameObject.Find("Main Camera").transform, false);
    }

    public void offDisplay()
    {
        gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
    }
}
