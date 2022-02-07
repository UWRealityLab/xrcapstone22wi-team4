using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabToSelect : MonoBehaviour
{
    public GameObject colorChangeTarget;

    // When object is selected
    public void OnGrabEnter()
    {
        var cylinderRenderer = colorChangeTarget.GetComponent<Renderer>();

        if (this.name == "apple")
        {
            cylinderRenderer.material.SetColor("_Color", Color.red);
            // call async load scene 1 script
            LoadNewScene("Assets/Scenes/scene_switch_1.unity");
        }
        else if (this.name == "avocado")
        {
            cylinderRenderer.material.SetColor("_Color", Color.green);
            // call async load scene 2 script
            LoadNewScene("Assets/Scenes/scene_switch_2.unity");
        }
        else if (this.name == "orange")
        {
            cylinderRenderer.material.SetColor("_Color", Color.yellow);
            // call async load scene 3 script
            LoadNewScene("Assets/Scenes/scene_switch_3.unity");
        }
        else if (this.name == "scene_cylinder")
        {
            // call async load main scene script
            LoadNewScene("Assets/Scenes/vr-menu-demo.unity");
        }

    }

    void LoadNewScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

}