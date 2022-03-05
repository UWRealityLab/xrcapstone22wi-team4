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

            // call load USA scene
            LoadNewScene("Assets/Scenes/02_24_2022_USA_Store.unity");

            // unfade orb
        }
        else if (this.name == "avocado")
        {
            // call load Mexico scene
            LoadNewScene("Assets/Scenes/02_24_2022_MEXICO_Store.unity");
        }
        else if (this.name == "orange")
        {
            // call load China scene
            LoadNewScene("Assets/Scenes/02_24_2022_CHINA_Store.unity");
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
        Text toggleCurrencyeText = GameObject.Find("Toggle_currency_variable").GetComponent<Text>();
        pricingManager.toggleCurrency();
        toggleCurrencyeText.text = pricingManager.getCurrency();
    }

}