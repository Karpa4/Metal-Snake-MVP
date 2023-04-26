using System.Collections;
using System.Collections.Generic;
using UnityEditor;


public class MyEditorTools : Editor
{
    [MenuItem("MyMenu/ReladScene")]
	public static void ReloadScene()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
