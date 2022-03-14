using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CheckOut : MonoBehaviour
{
    public GameObject receipt_prefab;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag != "Player") return;

        Debug.Log("Entered Checkout Zone");
        Transform cart_items = GameObject.Find("Cart_Items").transform;
        PricingManager pricingManager = GameObject.Find("PricingManager").GetComponent<PricingManager>();
        pricingManager.currency = pricingManager.receiptCurrency;

        double cost = 0;
        Dictionary<string, double> item_price = new Dictionary<string, double>();
        Dictionary<string, int> item_count = new Dictionary<string, int>();

        if (pricingManager.getLocation() == "USA")
        {
            PlayerHistory.USA = new List<string>();
        }
        else if (pricingManager.getLocation() == "CHINA")
        {
            PlayerHistory.CHINA = new List<string>();
        }
        else
        {
            PlayerHistory.MEXICO = new List<string>();
        }
        for (int i = 0; i < cart_items.childCount; i++)
        {
            Transform child = cart_items.GetChild(i);
            string displayName = child.GetComponent<GetPrice>().getName();
            string name = child.name.Split(' ')[0];

            if (child.GetComponent<GetPrice>() == null) continue;

            if (pricingManager.getLocation() == "USA")
            {
                PlayerHistory.USA.Add(name);
            } else if (pricingManager.getLocation() == "CHINA")
            {
                PlayerHistory.CHINA.Add(name);
            } else
            {
                PlayerHistory.MEXICO.Add(name);
            }

            double item_cost = child.GetComponent<GetPrice>().getPrice();

            if (!item_price.ContainsKey(displayName))
            {
                item_price.Add(displayName, item_cost);
            }
            
            if (!item_count.ContainsKey(displayName))
            {
                item_count.Add(displayName, 0);
            }
            item_count[displayName] = item_count[displayName] + 1;

            cost += child.GetComponent<GetPrice>().getPrice();
        }

        Debug.Log("total cost: " + cost);

        // create text receipt 
        Vector3 position = Vector3.zero;
        position.z += 1f;
        GameObject receipt = Instantiate(receipt_prefab, position, Quaternion.identity);
        receipt.transform.SetParent(GameObject.Find("Main Camera").transform, false);

        string text = "Receipt:\n";
        foreach (string item in item_price.Keys) {
            text += item + " x" + item_count[item] + "     " + item_price[item] + "\n";
        }
        text += "Total Cost:  " + cost + " " + pricingManager.getReceiptCurrency() + "\n\n";

        // goal cost difference
        double costDiff;
        string goalDiff;
        if (pricingManager.getLocation() == "USA")
        {
            costDiff = cost - 30;
            goalDiff = "Goal: 30 USD";
        }
        else if (pricingManager.getLocation() == "CHINA")
        {
            costDiff = cost - 190;
            goalDiff = "Goal: 190 yuan";
        }
        else
        {
            costDiff = cost - 620;
            goalDiff = "Goal: 620 pesos";
        }

        text += goalDiff + "\n";

        // determine goal diff
        if (costDiff < 0)
        {
            text += "You spent " + Mathf.Abs((float)costDiff) + " " + pricingManager.getReceiptCurrency() + " under the goal";
        } else
        {
            text += "You spent " + Mathf.Abs((float)costDiff) + " " + pricingManager.getReceiptCurrency() + " over the goal";
        }

        // set receipt text
        receipt.GetComponent<TextMeshPro>().text = text;

        if (pricingManager.getLocation() == "USA")
        {
            PlayerHistory.USA_Receipt = text;
        }
        else if (pricingManager.getLocation() == "CHINA")
        {
            PlayerHistory.CHINA_Receipt = text;
        }
        else
        {
            PlayerHistory.MEXICO_Receipt = text;
        }

        // load in the next scene after a few seconds
        StartCoroutine("loadNewScene");
    }

    IEnumerator loadNewScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Assets/Scenes/vr-menu-demo.unity");
    }
}
