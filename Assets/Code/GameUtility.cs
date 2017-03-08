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
        0, //  level 0 (intro), don't touch
        9, //  level 1
        12, //  level 2
        14, //  level 3
        12, //  level 4
        10, //  level 5
        14, //  level 6
        14, //  level 7
        19, //  level 8
        11, //  level 9
        17, // level 10
        17, // level 11
        16, // level 12
        17, // level 13
        19, // level 14
        15, // level 15
        22, // level 16
        31, // level 17
        17, // level 18
        15, // level 19 
        31, //  level 20
        0 // level 21 (credits)

    };
    public static int numLevels = 22;
    public static string lastScene;

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
        if (currentScene == "Help")
            return;
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
        if (currentScene == "Help")
            return;
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
        else if (Input.GetKeyDown(KeyCode.H))
            loadHelpScene();
        else if (Input.GetKeyDown(KeyCode.Escape))
            exitHelpScene();
    }

    public static void loadHelpScene()
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Help");
    }

    public static void exitHelpScene()
    {
        if (SceneManager.GetActiveScene().name == "Help")
            SceneManager.LoadScene(lastScene);
    }

    public static int getLevelPar()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string num = currentScene.Substring(5);
        int i = Int32.Parse(num);
        return levelPars[i];
    }


}

