using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
#if UNITY_STANDALONE_WIN
using AnotherFileBrowser.Windows;
#endif

public class FileBrowserTest : MonoBehaviour
{
    public Text resultText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// FileDialog for picking a single file
    /// </summary>
    public void OpenFileBrowser()
    {
#if UNITY_STANDALONE_WIN
        var bp = new BrowserProperties();
        bp.filter = "txt files (*.txt)|*.txt";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, result =>
        {
            resultText.text = result;
            Debug.Log(result);
        });
#endif
    }

    /// <summary>
    /// FileDialog for picking multiple file(s)
    /// </summary>
    public void OpenMultiSelectBrowser()
    {
#if UNITY_STANDALONE_WIN
        var bp = new BrowserProperties();
        bp.filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
        bp.filterIndex = 0;

        new FileBrowser().OpenMultiSelectFileBrowser(bp, result =>
        {
            string s = "";
            for (int i = 0; i < result.Length; i++)
            {
                s += result[i] + "\n";
            }
            resultText.text = s;
        });
#endif
    }

    /// <summary>
    /// FileDialog for selecting any folder
    /// </summary>
    public void OpenFolderBrowser()
    {
#if UNITY_STANDALONE_WIN
        var bp = new BrowserProperties();
        bp.filter = "txt files (*.txt)|*.txt";
        bp.filterIndex = 0;

        new FileBrowser().OpenFolderBrowser(bp, result =>
        {
            resultText.text = result;
            Debug.Log(result);
        });
#endif
    }

    /// <summary>
    /// FileDialog for saving any file, returns path with extention for further uses
    /// </summary>
    public void SaveFileBrowser()
    {
#if UNITY_STANDALONE_WIN
        var bp = new BrowserProperties();
        bp.filter = "txt files (*.txt)|*.txt";
        bp.filterIndex = 0;

        new FileBrowser().SaveFileBrowser(bp, "test", ".txt", result =>
        {
            resultText.text = result;
            Debug.Log(result);
        });
#endif
    }
}
