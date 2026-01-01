using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Objects to grab")] 
    
    [SerializeField]
    private Transform playerCamera;


    [Header("Variables to flip")]
    [SerializeField]
    private float maxRotation;

    private Quaternion originalRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalRotation = playerCamera.rotation;   
    }

    // Update is called once per frame
    void Update()
    {
        int maxDisplayWidth = Display.main.renderingWidth;
        int maxDisplayHeight = Display.main.renderingHeight;

        float mouseDX = 2*Input.mousePosition.x/maxDisplayWidth-1;
        float mouseDY = 1-2*Input.mousePosition.y/maxDisplayHeight;

        playerCamera.rotation = originalRotation*Quaternion.Euler(maxRotation*mouseDY,maxRotation*mouseDX,0);
    }
}
