using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class PressMenuButton : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject pauseMenu;
    public GameObject continueObject;
    public GameObject exitObject;
    public GameObject priceToggleObject;
    public GameObject currencyToggleObject;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();

        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(false);
        }
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }


    // Make pause menu appear whenever menu is pressed
    void CreatePauseMenu()
    {
        // check for menu button click
        if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuValue))
        {
            if (menuValue) {

                // set position of pause menu right in front of player
                Vector3 playerPos = playerTransform.position;
                Vector3 playerDirection = playerTransform.transform.forward;
                float menuDistance = 0.31f;

                Vector3 menuPosition = playerPos + (playerDirection * menuDistance);

                // fiddle with the menu height if it's weird
                Vector3 yOffset = (transform.up * 1.6f);

                pauseMenu.transform.position = menuPosition;

                // sets pause menu rotation to follow camera head
                pauseMenu.transform.SetParent(GameObject.Find("Main Camera").transform, false);

                // set all grabbables and menu active
                pauseMenu.SetActive(true);
                continueObject.SetActive(true);
                exitObject.SetActive(true);
                priceToggleObject.SetActive(true);
                currencyToggleObject.SetActive(true);


                // Show toggle text according to other changed variables
                Text toggleCurrencyText = GameObject.Find("Toggle_currency_variable").GetComponent<Text>();
                PricingManager pricingManager = GameObject.Find("PricingManager").GetComponent<PricingManager>();
                toggleCurrencyText.text = pricingManager.getCurrency();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            CreatePauseMenu();
        }
    }
}
