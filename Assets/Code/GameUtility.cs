using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameUtility
{
    public static int numLevels = 14;

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

}

