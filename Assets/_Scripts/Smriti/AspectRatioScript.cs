using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectRatioScript : MonoBehaviour
{
    public float aspectRatio = 16.0f / 9.0f;
    private Camera cameraObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraObj = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        setAspectRatio();
    }

    void setAspectRatio()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / aspectRatio;

        if (scaleHeight < 1.0f)
        {
            Rect rect = cameraObj.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (float)(1.0 - scaleHeight) / 2.0f;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = cameraObj.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (float)(1.0 - scaleWidth) / 2.0f;
            rect.y = 0;

            cameraObj.rect = rect;
        }
    }
}
