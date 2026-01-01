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

    [Header("Variables to flip")]
    [SerializeField]
    private float maxSlideTime;

    /*Private variables*/
    private Vector3 leftOGPos;

    private Vector3 mainOGPos;

    private Vector3 rightOGPos;

    private float slideTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftOGPos = leftTransform.position;
        mainOGPos = mainTranform.position;
        rightOGPos = rightTransform.position;
    }

    void resetPosition()
    {
        leftTransform.position = leftOGPos;
        mainTranform.position = mainOGPos;
        rightTransform.position = rightOGPos;
        addCardTransform.position = new Vector3(-20f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (slideTime == 0f)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                slideTime += Time.deltaTime;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                slideTime -= Time.deltaTime;
            }
        }

        if (slideTime > maxSlideTime || slideTime < -maxSlideTime)
        {
            slideTime = 0f;

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
