using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PressMenuButton : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject pauseMenu;
    public GameObject continueObject;
    public GameObject exitObject;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
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
                Quaternion playerRotation = playerTransform.rotation;
                float menuDistance = 0.5f;

                Vector3 menuPosition = playerPos + (playerDirection * menuDistance);

                pauseMenu.transform.position = menuPosition;
                pauseMenu.transform.rotation = playerRotation;

                pauseMenu.SetActive(true);
                continueObject.SetActive(true);
                exitObject.SetActive(true);
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
