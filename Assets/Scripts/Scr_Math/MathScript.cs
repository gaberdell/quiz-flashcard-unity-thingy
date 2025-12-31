using UnityEngine;

public class MathScript
{

    public static float CubicEaseOut(float value)
    {
        if (value > 0)
        {
            float x = 1f - value;
            return 1f - x*x*x;
        }
        else
        {
            float x = 1f + value;
            return -1f + x*x*x;
        }
    }


    //Meant only to work on -1-1 values
    public static float SquareFinction(float value)
    {
        return Mathf.Sign(value) * value * value;
    }

    //Meant only to work on 0-1 values 
    public static float ElasticFunction(float elastic)
    {
        
        if (elastic < 0.34043)
        {
            float x = elastic - 0.167f;

            elastic = 14.1097280022f * x * x - 0.3935f;
        }
        else
        {
            float x = elastic - 1f;
            elastic = -2.22766763199f * x * x + 1f;
        }

        return elastic;
    }
}
