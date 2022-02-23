using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor;

public class GetPrice : MonoBehaviour
{
    // Start is called before the first frame update
    public double cost;
    public string displayName;

    void Start()
    {
        //gameObject.GetComponent<XRAObjectGrabInteractable>().interactionManager = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
        string name = gameObject.name.Split(' ')[0].Replace("(Clone)", " ");
        GameObject pricingManager = GameObject.Find("PricingManager");
        cost = pricingManager.GetComponent<PricingManager>().getPrice(name);
    }

    public double getPrice()
    {
        return cost;
    }

    public string getName()
    {
        return displayName;
    }
}
