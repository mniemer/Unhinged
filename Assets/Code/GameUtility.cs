using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameUtility
{


    public static int[] levelPars =
    {
        0, //  level 0, don't touch
        1, //  level 1
        2, //  level 2
        3, //  level 3
        4, //  level 4
        5, //  level 5
        6, //  level 6
        7, //  level 7
        8, //  level 8
        9, //  level 9
        10, // level 10
        11, // level 11
        12, // level 12
        13, // level 13
        14, // level 14
        15, // level 15
        16, // level 16
        17, // level 17
        18, // level 18
        19, // level 19
        20 //  level 20

    };
    public static int numLevels = 21;

    public static int gameToGridCoord(float coord)
    {
        return Convert.ToInt32(coord + 9.5f);
    }

    public static float gridToGameCoord(int coord)
    {
        return (coord - 9.5f);
    }

    public static bool areBlocksAllStill()
    {
        foreach (BlockController blk in GameObject.FindObjectsOfType<BlockController>())
        {
            if (blk.moving)
            {
                return false;
            }
        }
        return true;
    }
    

    public static void loadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string num = currentScene.Substring(5);
        int i = Int32.Parse(num);
        if (i+1 < numLevels)
        {
            string next = (i + 1).ToString();
            string nextLevel = "Level" + next;
            SceneManager.LoadScene(nextLevel);
        }
    }

    public static void loadPreviousLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string num = currentScene.Substring(5);
        int i = Int32.Parse(num);
        if (i > 0)
        {
            string next = (i - 1).ToString();
            string nextLevel = "Level" + next;
            SceneManager.LoadScene(nextLevel);
        }
    }

    public static void reloadCurrentLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public static void HandleSceneInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            reloadCurrentLevel();
        else if (Input.GetKeyDown(KeyCode.N))
            loadNextLevel();
        else if (Input.GetKeyDown(KeyCode.P))
            loadPreviousLevel();
    }

    public static int getLevelPar()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string num = currentScene.Substring(5);
        int i = Int32.Parse(num);
        return levelPars[i];
    }


}

