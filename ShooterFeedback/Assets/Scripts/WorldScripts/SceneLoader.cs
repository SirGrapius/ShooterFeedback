using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Movement player;
    [SerializeField] AudioController AC;
    void Start()
    {
        player = GetComponent<Movement>();
    }


    void Update()
    {
        if (player.health <= 0)
        {
            StartCoroutine(ScreenLoadProcess(1));
        }
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
                    AC.playMusic(AC.audios[0]); //change audio to victory music
                    break;
                }

            //lose
            case 1:
                {
                    yield return new WaitForSeconds(5);
                    SceneManager.LoadScene("GameOverScreen");
                    AC.playMusic(AC.audios[0]); //change audio to death music
                    break;
                }
        }
    }
}
