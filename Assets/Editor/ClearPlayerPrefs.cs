using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : EditorWindow {

    [MenuItem("Tools/ClearPlayerPrefs")]
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
