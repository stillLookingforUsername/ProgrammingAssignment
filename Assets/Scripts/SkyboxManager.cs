using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public float skySpeed;  //speed at which the skybox will be moving
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation",Time.time * skySpeed);
    }
}
