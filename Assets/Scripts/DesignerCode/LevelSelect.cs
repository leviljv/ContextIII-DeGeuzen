using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public void WillemLevel()
    {
        SceneManager.LoadScene("1_AreaWillem");
    }

    public void KetterBevrijd()
    {
        SceneManager.LoadScene("2_KetterBevrijdStad");
    }

    public void Beeldenstorm()
    {
        SceneManager.LoadScene("2b_BeeldenstormStad");
    }

    public void MoordBoer()
    {
        SceneManager.LoadScene("2c_MoordBoerStad");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("0a_MainMenu");
    }
}
