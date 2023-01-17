using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneStartGame : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("0c_Tutorial1");
    }
}
