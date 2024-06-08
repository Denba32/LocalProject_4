using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager instance;

    public static DataManager Instance
    {
        get => instance ?? (instance = new DataManager()); 
    }


}