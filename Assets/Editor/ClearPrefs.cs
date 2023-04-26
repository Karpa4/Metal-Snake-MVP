using UnityEngine;
using UnityEditor;

public class ClearPrefs
{
    [MenuItem("Custom Tools/Clear Prefs")]
    public static void DelPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs cleared!");
    }
}
