using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrabToSelect : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject continueObject;
    public GameObject exitObject;
    public GameObject togglePriceObject;
    public GameObject toggleCurrencyObject;

    // When object is selected
    public void OnGrabEnter()
    {
        if (this.name == "apple")
        {
            // call load USA script
            LoadNewScene("Assets/Scenes/USAStoreFinalTiny.unity");
        }
        else if (this.name == "avocado")
        {
            // call load Mexico script
            LoadNewScene("Assets/Scenes/MEXICOStoreFinalTiny.unity");
        }
        else if (this.name == "orange")
        {
            // call load China script
            LoadNewScene("Assets/Scenes/CHINAStoreFinalTiny.unity");
        }
        else if (this.name == "scene_cylinder")
        {
            // call load main scene script
            LoadNewScene("Assets/Scenes/vr-menu-demo.unity");
        }
    }

    public void onPauseMenuGrabEnter()
    {
        // Load the main menu scene
        if (SceneManager.GetActiveScene().path != "Assets/Scenes/vr-menu-demo.unity")
        {
            LoadNewScene("Assets/Scenes/vr-menu-demo.unity");
        } else
        {
            pauseMenu.SetActive(false);
            continueObject.SetActive(false);
            exitObject.SetActive(false);
        }
    }


    void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Change price visibility toggle
    public void onPriceToggleGrabEnter()
    {
        PricingManager pricingManager = GameObject.Find("PricingManager").GetComponent<PricingManager>();
        pricingManager.toggleDisplay();
        Text togglePriceText = GameObject.Find("Toggle_price_variable").GetComponent<Text>();
        if (togglePriceText.text == "On")
        {
            togglePriceText.text = "Off";
        } else
        {
            togglePriceText.text = "On";
        }
    }

    // Change currency type in text
    public void onCurrencyToggleGrabEnter()
    {
        PricingManager pricingManager = GameObject.Find("PricingManager").GetComponent<PricingManager>();
        pricingManager.toggleCurrency();
        Text toggleCurrencyeText = GameObject.Find("Toggle_currency_variable").GetComponent<Text>();
        if (toggleCurrencyeText.text == "Yuan")
        {
            toggleCurrencyeText.text = "Peso";
        }
        if (toggleCurrencyeText.text == "Peso")
        {
            toggleCurrencyeText.text = "USD";
        }
        if (toggleCurrencyeText.text == "USD")
        {
            toggleCurrencyeText.text = "Yuan";
        }
    }

}