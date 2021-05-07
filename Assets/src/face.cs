using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class face : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        SceneManager.LoadScene("game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Starts()
    {
        SceneManager.LoadScene("start");
    }
}
