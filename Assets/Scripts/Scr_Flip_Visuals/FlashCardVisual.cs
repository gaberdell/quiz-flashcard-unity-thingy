using UnityEngine;
using TMPro;
using System;

public class FlashCardVisual : MonoBehaviour
{
    [Header("Objects to grab")] 
    
    [SerializeField]
    private Transform flipTransform;

    [SerializeField]
    private TextMeshPro frontText;

    [Header("Variables to flip")]
    [SerializeField]
    private float flipMaxTime;

    [SerializeField]
    private float flipAngle;

    [SerializeField]
    private float angleOffset;


    Func<float, float> zeroToOneSwitch;

    /*Private variables*/
    private float currentFlip = 0; /* Flip rotation is represented by a range from -1,1 */

    private bool isAdding = true;

    private bool isActivated = false;

    void Start()
    {
        zeroToOneSwitch = MathScript.CubicEaseOut;
    }

    public float GetCurrentFlipTime()
    {
        return currentFlip;
    }

    public void SetCurrentFlip(float cF)
    {
        currentFlip = cF;

        float flipRotation = zeroToOneSwitch(currentFlip / flipMaxTime) * flipAngle;

        flipTransform.rotation = Quaternion.Euler(angleOffset + flipRotation, 0f, 0f);
    }

    public void ActivateFlippage(bool newActiavtion)
    {
        isActivated = newActiavtion;

        if (!isActivated)
        {
            isAdding = true;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isActivated)
        {
            isAdding = !isAdding;
        }

        float addingTogether = isAdding ? 1f: -1f;

        currentFlip = Math.Clamp(currentFlip + Time.deltaTime * addingTogether, -flipMaxTime, flipMaxTime);

        float flipRotation = zeroToOneSwitch(currentFlip / flipMaxTime) * flipAngle;

        flipTransform.rotation = Quaternion.Euler(angleOffset + flipRotation, 0f, 0f);

    }
}
