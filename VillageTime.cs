using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTime : MonoBehaviour
{
    [SerializeField]
    private Material skyboxDay;
    [SerializeField]
    private Material skyboxAnoon;
    [SerializeField]
    private Material skyboxNight;
    [SerializeField]
    private Light WordlLight;
    public static VillageTime Instance;
    private System.DateTime Hour = System.DateTime.Now;

    public void Update()
    {
        if(Application.isPlaying)
        {
            if (Hour.Hour >= 18 && Hour.Hour < 6)
            {
                WordlLight.intensity = 0;
                RenderSettings.skybox = skyboxNight;
                WordlLight.color = new Color(0, 0, 0);
                WordlLight.shadows = LightShadows.None;
            }
            if (Hour.Hour >= 6 && Hour.Hour < 13)
            {
                WordlLight.intensity = 7.5f;
                RenderSettings.skybox = skyboxDay;
                WordlLight.shadows = LightShadows.Soft;
                WordlLight.color = new Color(0.233f, 0.215f, 0.215f);
            }
            if (Hour.Hour >= 13 && Hour.Hour < 18)
            {
                WordlLight.intensity = 7.5f;
                RenderSettings.skybox = skyboxAnoon;
                WordlLight.shadows = LightShadows.Hard;
                WordlLight.color = new Color(0.188f, 0.132f, 0.132f);
            }
        }
    }
}
