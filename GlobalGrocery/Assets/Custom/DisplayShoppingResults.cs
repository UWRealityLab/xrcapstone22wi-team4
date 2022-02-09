using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using TMPro;

public class DisplayShoppingResults : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> items = null;
        string receipt = gameObject.name + "\n";
        if (gameObject.name == "CHINA")
        {
            items = PlayerHistory.CHINA;
            receipt = PlayerHistory.CHINA_Receipt;

        }
        if (gameObject.name == "MEXICO")
        {
            items = PlayerHistory.MEXICO;
            receipt = PlayerHistory.MEXICO_Receipt;
        }
        if (gameObject.name == "USA")
        {
            items = PlayerHistory.USA;
            receipt = PlayerHistory.USA_Receipt;
        }

        if (items == null)
        {
            TextMeshPro textt = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
            textt.text = receipt;
            return;
        }

        TextMeshPro text = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        text.text = receipt;
        StartCoroutine("spawnWait", items);
    }

    IEnumerator spawnWait(List<string> items)
    {
        foreach (string name in items)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log(name);
            Object loadPrefab = Resources.Load("Produce/" + name);
            if (loadPrefab == null)
            {
                loadPrefab = Resources.Load("Products/" + name);
            }
            if (loadPrefab == null)
            {
                loadPrefab = Resources.Load("Products/Other/" + name);
            }
            if (loadPrefab == null) continue;

            Vector3 pos = gameObject.transform.position;
            pos.y += 2;
            GameObject item = (GameObject) Instantiate(loadPrefab, pos, Quaternion.identity);
        }
    }
}
