using UnityEngine;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System;

public class ImportButton : MonoBehaviour
{
    //ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = "/bin/bash", Arguments = "zenity --file-selection ", }; 

    //Zenity window looks pretties so we try to make one first. If not possible create an xde window instead.
    ProcessStartInfo isZenity = new ProcessStartInfo()  
    {  
        FileName = "bash",         // Use bash to execute the command  
        Arguments = "-c 'zenity --version'",  // "-c" tells bash to run the following string as a command  
        RedirectStandardOutput = true,  // Capture output  
        RedirectStandardError = true,   // Capture errors  
        UseShellExecute = false,   // Required for output redirection  
        CreateNoWindow = true      // Run in background (no visible terminal)  
    };  

    ProcessStartInfo zenityGrabCommand = new ProcessStartInfo()  
    {  
        FileName = "bash",         // Use bash to execute the command  
        Arguments = "-c 'zenity --file-selection'",  // "-c" tells bash to run the following string as a command  
        RedirectStandardOutput = true,  // Capture output  
        RedirectStandardError = true,   // Capture errors  
        UseShellExecute = false,   // Required for output redirection  
        CreateNoWindow = true      // Run in background (no visible terminal)  
    };

    [DllImport("gtkBasicWindowMaker.so")]
    public static extern unsafe char* activate();

    [DllImport("gtkBasicWindowMaker.so")]
    public static extern void freeCharResult();


    public void MakeMenu()
    {
        UnityEngine.Debug.Log(CreateFolderWindow());
    }

    public string CreateFolderWindow()
    {
        bool isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        if (isLinux)
        {
            return CreateLinuxWindow();
        }

        return "";
    }


    public string CreateLinuxWindow()
    {
        using (Process process =  Process.Start(isZenity))
        {
            process.WaitForExit();  
 
            if (process.ExitCode == 0)
            {
                return CreateXDEWindow();
            }
            else
            {
                return CreateZenityWindow();
            }
        }
    }

    public string CreateZenityWindow()
    {
        UnityEngine.Debug.Log("Amogus");
        using (Process process =  Process.Start(zenityGrabCommand))
        {
            string output = process.StandardOutput.ReadToEnd();  
            string error = process.StandardError.ReadToEnd(); 


            // Wait for the process to finish  
            process.WaitForExit();  
 
            // Print results  
            UnityEngine.Debug.Log("Output:");  
            UnityEngine.Debug.Log(output);  
            UnityEngine.Debug.Log("Errors:");  
            UnityEngine.Debug.Log(error);  
            UnityEngine.Debug.Log($"Exit Code: {process.ExitCode}"); // 0 = success, non-zero = failure
            return output;
        }
    }

    public string CreateXDEWindow()
    {
        string xdeString;
        unsafe
        {
            xdeString = Marshal.PtrToStringAnsi((IntPtr)activate());
        }
        freeCharResult();
        return xdeString;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
