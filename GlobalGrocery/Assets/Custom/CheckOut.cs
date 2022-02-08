using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckOut : MonoBehaviour
{
    public GameObject receipt_prefab;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag != "Player") return;

        Debug.Log("Entered Checkout Zone");
        Transform cart_items = GameObject.Find("Cart_Items").transform;

        double cost = 0;
        Dictionary<string, double> item_price = new Dictionary<string, double>();
        Dictionary<string, int> item_count = new Dictionary<string, int>();
        for (int i = 0; i < cart_items.childCount; i++)
        {
            Transform child = cart_items.GetChild(i);
            string name = child.name.Split(' ')[0];
            Debug.Log(name);
            if (child.GetComponent<GetPrice>() == null) continue;
            double item_cost = child.GetComponent<GetPrice>().getPrice();

            if (!item_price.ContainsKey(name))
            {
                item_price.Add(name, item_cost);
            }
            
            if (!item_count.ContainsKey(name))
            {
                item_count.Add(name, 0);
            }
            item_count[name] = item_count[name] + 1;

            cost += child.GetComponent<GetPrice>().getPrice();
        }

        Debug.Log("total cost: " + cost);

        // create text receipt 
        Transform player = GameObject.Find("Cart_nabnuh").transform;
        Vector3 position = Vector3.zero;
        position.z += 1f;
        position.y += 1f;
        GameObject receipt = Instantiate(receipt_prefab, position, Quaternion.identity);
        receipt.transform.SetParent(GameObject.Find("XR Origin").transform, false);

        string text = "Receipt:\n";
        foreach (string item in item_price.Keys) {
            text += item + " x" + item_count[item] + "       " + item_price[item] + "\n";
        }
        text += "Total Cost:      " + cost + " USD";
        Debug.Log(text);
        receipt.GetComponent<TextMeshPro>().text = text;

        // load in the next scene after a few seconds
    }
}
