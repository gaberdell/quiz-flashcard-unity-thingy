using UnityEngine;
using System.Runtime.InteropServices;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    float helloDoWeWOrk;


    [DllImport("gtkBasicWindowMaker.so")]
    public static extern void freeCharResult();

    [DllImport("gtkBasicWindowMaker.so")]
    public static extern unsafe char* activate();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        freeCharResult();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
