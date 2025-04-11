using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = 1;
    public Vector2 newPlayerPosition;
    private Transform player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;
            FadeToBlack();
        }    
    }

    public void FadeToBlack()
    {
        fadeAnim.Play("FadeToBlack");
        StartCoroutine(DelayFade());
    }

    public void FadeFromBlack()
    {
        fadeAnim.Play("FadeFromBlack");
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        player.position = newPlayerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }
}
