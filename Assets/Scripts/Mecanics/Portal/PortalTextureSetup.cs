using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    [SerializeField] public List<PortalTextureSetupData> portalTextureSetupDataList;

    void Start()
    {
        foreach (PortalTextureSetupData SetupData in portalTextureSetupDataList)
        {
            if (SetupData.camera.targetTexture != null)
            {
                SetupData.camera.targetTexture.Release();
            }
            SetupData.camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.sRGB);
            SetupData.camera.targetTexture.Create();
            SetupData.cameraMat.mainTexture = SetupData.camera.targetTexture;
        }
    }
}

[System.Serializable]
public class PortalTextureSetupData
{
    public Camera camera;
    public Material cameraMat;
}