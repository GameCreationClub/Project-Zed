using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public Texture2D[] levels;

    private LevelGenerator levelGenerator;

    private int currentLevel = 0;

    private void Start()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        levelGenerator.map = levels[0];
        levelGenerator.GenerateLevel();
    }

    public void NextLevel()
    {
        if (currentLevel < levels.Length - 1)
        {
            currentLevel++;
            levelGenerator.map = levels[currentLevel];
            levelGenerator.GenerateLevel();
        }
    }
}
