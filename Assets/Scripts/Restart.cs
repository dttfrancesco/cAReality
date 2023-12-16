﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartTrigger()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
