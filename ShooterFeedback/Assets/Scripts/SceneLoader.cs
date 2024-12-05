using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WinBlock")
        {
            StartCoroutine(ScreenLoadProcess(0));
        }
    }

    IEnumerator ScreenLoadProcess(int WinOrLose)
    {
        switch(WinOrLose)
        {
            //win
            case 0:
                {
                    yield return new WaitForSeconds(5);
                    SceneManager.LoadScene("WinScreen");
                    break;
                }

            //lose
            case 1:
                {
                    yield return new WaitForSeconds(5);
                    SceneManager.LoadScene("GameOverScreen");
                    break;
                }
        }
    }
}
