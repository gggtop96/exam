using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private static ControllerManager Instance = null;

    public static ControllerManager GetInstace()
    {
        if (Instance == null)
            Instance = new ControllerManager();
        return Instance;
    }

    public bool DirRight;

   public bool DirLeft;
    private ControllerManager()
    {

    }
}
