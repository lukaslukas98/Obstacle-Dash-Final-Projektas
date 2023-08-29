using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGoingDown : MonoBehaviour {
    
    public GameObject directionalLight;
    public Transform LevelEngine;
    public new Camera camera;
    public GameObject obstacleGenerator;

    private bool LightsAreOut = false;
    
    
    
    // Update is called once per frame
    void Update () {
        directionalLight.GetComponent<Transform>().LookAt(LevelEngine);
        directionalLight.GetComponent<Transform>().position -= new Vector3(0, 0.03f, 0);
        if (directionalLight.GetComponent<Transform>().rotation.x < 0 && LightsAreOut==false)
        {
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientSkyColor = new Color(0.394f, 0.479f, 0.661f);
            LightsOut();
            obstacleGenerator.SetActive(true);
            LightsAreOut = true;
        }
    }

    int ChangesMade = 0;
    public void LightsOut()
    {
        if (directionalLight.GetComponent<Light>().intensity > 0)
        {
            directionalLight.GetComponent<Light>().intensity -= 0.01f;
        }

        if (RenderSettings.ambientIntensity > 0)
        {
            RenderSettings.ambientIntensity -= 0.01f;
        }

        if (RenderSettings.reflectionIntensity > 0)
        {
            RenderSettings.reflectionIntensity -= 0.01f;
        }

        if (RenderSettings.ambientSkyColor.b > 0)
        {
            RenderSettings.ambientSkyColor = RenderSettings.ambientSkyColor - new Color(0.01f, 0.01f, 0.01f);
        }

        if (RenderSettings.fogColor.r > 0)
        {
            RenderSettings.fogColor = RenderSettings.fogColor - new Color(0.01f, 0.01f, 0.01f);
        }

        if (camera.backgroundColor.r > 0)
        {
            camera.backgroundColor = camera.backgroundColor - new Color(0.01f, 0.01f, 0.01f);
        }

        if (ChangesMade<200)
        {
            ChangesMade++;
            Invoke("LightsOut",0.01f);
        }
    }
}
