using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class PricingManager : MonoBehaviour
{
    public string location = "USA"; // CHINA, MEXICO, USA
    public string currency = "USA"; // CHINA, MEXICO, USA
    public string receiptCurrency = "USA";
    public bool display = false;

    private Dictionary<string, double> prices_USA;
    private Dictionary<string, double> prices_CHINA;
    private Dictionary<string, double> prices_MEXICO;
    private Dictionary<string, string> displayNames;

    // conversions as of 2/6/2022
    private double CHINA_to_MEXICO = 8.05;
    private double CHINA_to_USA = 0.16;
    private double MEXICO_to_CHINA = 0.12;
    private double MEXICO_to_USA = 0.048;
    private double USA_to_CHINA = 6.36;
    private double USA_to_MEXICO = 20.67;

    // Start is called before the first frame update
    void Awake()
    {
        prices_USA = new Dictionary<string, double>();
        prices_CHINA = new Dictionary<string, double>();
        prices_MEXICO = new Dictionary<string, double>();
        displayNames = new Dictionary<string, string>();

        var file = Resources.Load<TextAsset>("CSVs/Pricing_CSV");
        string text = file.text;
        string[] lines = text.Split('\n');
        
        for (int i = 1; i < lines.Length; i++) {
            var line = lines[i]; //reader.ReadLine();
            var values = line.Split(',');
            if (values.Length < 4) continue;
            string item = values[0];
            double price_usa = Convert.ToDouble(values[1]);
            double price_china = Convert.ToDouble(values[2]);
            double price_mexico = Convert.ToDouble(values[3]);

            prices_USA.Add(item, price_usa);
            prices_CHINA.Add(item, price_china);
            prices_MEXICO.Add(item, price_mexico);

            // add display info
            if (values.Length == 5 && !values[4].Contains("0")) // has alt name
            {
                string cleanedName = Regex.Replace(values[4], "[^A-Za-z0-9 -]", "");
                Debug.Log(cleanedName);
                displayNames.Add(item, cleanedName);
            }

        }
    }

    public string getCurrency()
    {
        if (currency == "USA")
        {
            return "USD";
        } else if (currency == "CHINA")
        {
            return "Yuan";
        } else if (currency == "MEXICO")
        {
            return "Peso";
        }
        return "";
    }

    public string getReceiptCurrency()
    {
        if (currency == "USA")
        {
            return "USD";
        }
        else if (currency == "CHINA")
        {
            return "Yuan";
        }
        else if (currency == "MEXICO")
        {
            return "Peso";
        }
        return "";
    }

    public double getPrice(String item)
    {
        double cost = 0.0;
        item = item.Split(' ')[0].Replace("(Clone)", " ");

        if (!prices_CHINA.ContainsKey(item))
        {
            return 3.2;
        }

        if (location.Equals("CHINA"))
        {
            cost = prices_CHINA[item] * getScaler();
        } else if (location.Equals("MEXICO"))
        {
            cost = prices_MEXICO[item] * getScaler();
        } else if (location.Equals("USA"))
        {
            cost = prices_USA[item] * getScaler();
        } else
        {
            Debug.Log("location not recognized");
        }
        return Math.Round(cost * getScaler(), 2);
    }

    public string getDisplayName(String item)
    {
        item = item.Split(' ')[0].Replace("(Clone)", " ");
        string displayName = item;
        if (displayNames.ContainsKey(item))
        {
            displayName = displayNames[item];
        }
        return displayName;
    }

    public string getLocation()
    {
        return location;
    }

    private double getScaler()
    {
        // location equals display currency
        if (location.Equals(currency))
        {
            return 1;
        } 
        else if (location.Equals("CHINA") && currency.Equals("MEXICO"))
        {
            return CHINA_to_MEXICO;
        }
        else if (location.Equals("CHINA") && currency.Equals("USA"))
        {
            return CHINA_to_USA;
        }
        else if (location.Equals("MEXICO") && currency.Equals("CHINA"))
        {
            return MEXICO_to_CHINA;
        }
        else if (location.Equals("MEXICO") && currency.Equals("USA"))
        {
            return MEXICO_to_USA;
        }
        else if (location.Equals("USA") && currency.Equals("CHINA"))
        {
            return USA_to_CHINA;
        }
        else if (location.Equals("USA") && currency.Equals("MEXICO"))
        {
            return USA_to_MEXICO;
        }

        return 0;
    }

    public void toggleDisplay()
    {
        display = !display;
    }

    public void toggleCurrency()
    {
        if (currency.Equals("CHINA"))
        {
            currency = "MEXICO";
        } else if (currency.Equals("MEXICO"))
        {
            currency = "USA";
        } else if (currency.Equals("USA"))
        {
            currency = "CHINA";
        }
    }
}
