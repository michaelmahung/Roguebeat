using System;
using UnityEngine;

//Playerprefs are limited in the data that they can store so im converting all bools to intergers.
//1 = true/active
//0 = false/deactive
//Right now this is set up specifically for the weapons but it can be expanded upon later.

public static class DataManager
{
    public static void SetPref(string key, bool value)
    {
        int prefValue = 0;

        if (value)
        {
            prefValue = 1;
        }

        PlayerPrefs.SetInt(key, prefValue);
    }

    public static bool GetPref(string key)
    {
        var value = PlayerPrefs.GetInt(key);

        if (value == 1)
        {
            return true;
        }

        return false;
    }

    public static bool HasPref(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return true;
        }

        return false;
    }
}
