using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleportGate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(PlayEffect());
    }
    IEnumerator PlayEffect()
    {

        yield return new WaitForSeconds(0.6f); //等待6S后切换界面
        SceneManager.LoadScene("game2");//需要切换的界面


    }
}
