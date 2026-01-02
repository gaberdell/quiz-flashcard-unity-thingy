using System;
using TMPro;
using UnityEngine;

public class FlashCardSlideController : MonoBehaviour
{
    [Header("Objects to grab")] 
    
    [SerializeField]
    private Transform leftTransform;

    [SerializeField]
    private Transform mainTranform;

    [SerializeField]
    private Transform rightTransform;

    [SerializeField]
    private Transform addCardTransform;

    [SerializeField]
    private Vector3 offsetForNewCards;


    [SerializeField]
    private TextMeshPro leftText;

    [SerializeField]
    private TextMeshPro mainText;

    [SerializeField]
    private TextMeshPro rightText;

    [SerializeField]
    private TextMeshPro newText;

    [SerializeField]
    private TextMeshPro leftBackText;

    [SerializeField]
    private TextMeshPro mainBackText;

    [SerializeField]
    private TextMeshPro rightBackText;

    [SerializeField]
    private TextMeshPro newBackText;

    [SerializeField]
    private FlashCardVisual leftVisual;

    [SerializeField]
    private FlashCardVisual mainVisual;

    [SerializeField]
    private FlashCardVisual rightVisual;

    [Header("Variables to flip")]
    [SerializeField]
    private float maxSlideTime;

    /*Private variables*/
    private Vector3 leftOGPos;

    private Vector3 mainOGPos;

    private Vector3 rightOGPos;

    private float slideTime = 0f;

    private int currentIndex = 0;

    private String[] frontFaceCards = {"présent je/j'", "présent tu", "présent il/elle/on", "présent nous", "présent vous", "présent ils/elles"};

    private String[] backFacingCards = {"vais", "vas", "va", "allons", "allez", "vont"};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftOGPos = leftTransform.position;
        mainOGPos = mainTranform.position;
        rightOGPos = rightTransform.position;

        resetPosition();
    }

    void resetPosition()
    {
        slideTime = 0f;

        leftTransform.position = leftOGPos;
        mainTranform.position = mainOGPos;
        rightTransform.position = rightOGPos;
        addCardTransform.position = new Vector3(-20f,0f,0f);

        mainText.text = frontFaceCards[currentIndex];
        leftText.text = frontFaceCards[listSafeSubtract()];
        rightText.text = frontFaceCards[listSafeAdd()];

        mainBackText.text = backFacingCards[currentIndex];
        leftBackText.text = backFacingCards[listSafeSubtract()];
        rightBackText.text = backFacingCards[listSafeAdd()];

        mainVisual.ActivateFlippage(true);
    }

    int listSafeSubtract()
    {
        return MathScript.ListSafeSubtract(currentIndex, 1, frontFaceCards.Length);
    }

    int listSafeAdd()
    {
        return MathScript.ListSafeAdd(currentIndex, 1, frontFaceCards.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (slideTime == 0f)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                slideTime += Time.deltaTime;
                currentIndex = listSafeSubtract();

                newText.text = frontFaceCards[listSafeSubtract()];
                newBackText.text = backFacingCards[listSafeSubtract()];

                mainVisual.ActivateFlippage(false);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                slideTime -= Time.deltaTime;
                currentIndex = listSafeAdd();

                newText.text = frontFaceCards[listSafeAdd()];
                newBackText.text = backFacingCards[listSafeAdd()];

                mainVisual.ActivateFlippage(false);
            }
        }

        if (slideTime > maxSlideTime)
        {
            rightVisual.SetCurrentFlip(mainVisual.GetCurrentFlipTime());
            mainVisual.SetCurrentFlip(leftVisual.GetCurrentFlipTime());
            
            resetPosition();
        }
        else if (slideTime < -maxSlideTime)
        {
            leftVisual.SetCurrentFlip(mainVisual.GetCurrentFlipTime());
            mainVisual.SetCurrentFlip(rightVisual.GetCurrentFlipTime());
            
            resetPosition();
        }
        else if (slideTime > 0f)
        {
            slideTime += Time.deltaTime;

            float curTime = slideTime/maxSlideTime;

            mainTranform.position = Vector3.Lerp(mainOGPos, rightOGPos, curTime);
            leftTransform.position = Vector3.Lerp(leftOGPos, mainOGPos, curTime);

            addCardTransform.position = Vector3.Lerp(leftOGPos - offsetForNewCards, leftOGPos, curTime);
            rightTransform.position = Vector3.Lerp(rightOGPos, rightOGPos + offsetForNewCards, curTime);
        }
        else if (slideTime < 0f)
        {
            slideTime -= Time.deltaTime;

            float curTime = -slideTime/maxSlideTime;

            mainTranform.position = Vector3.Lerp(mainOGPos, leftOGPos, curTime);
            rightTransform.position = Vector3.Lerp(rightOGPos, mainOGPos, curTime);

            addCardTransform.position = Vector3.Lerp(rightOGPos + offsetForNewCards, rightOGPos, curTime);
            leftTransform.position = Vector3.Lerp(leftOGPos, leftOGPos - offsetForNewCards, curTime);
        }

    }
}
