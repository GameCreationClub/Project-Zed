using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Crosstales.FB;

public class LevelTesting : MonoBehaviour
{
    public GameObject objectList;

    private LevelGenerator levelGenerator;

    private void Start()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    public void OpenFile()
    {
        string path;
        try
        {
            path = FileBrowser.OpenSingleFile();

            Texture2D map = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            map.LoadImage(File.ReadAllBytes(path));

            levelGenerator.map = map;
            levelGenerator.GenerateLevel();

            FindObjectOfType<PlayerParameters>().GetPlayer();
        }
        catch
        {
            Debug.LogError("Couldn't open file");
        }
    }

    public void ToggleObjectList(bool toggle)
    {
        objectList.SetActive(toggle);
    }
}
