using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject plant;

    [HideInInspector] public Vector2 _playerRespawnPos;
    [HideInInspector] public Vector2 _plantRespawnPos;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        _playerRespawnPos = player.transform.position;
        _plantRespawnPos = plant.transform.position;
    }


    public void Checkpoint(Vector2 position)
    {
        _playerRespawnPos = position;
        _plantRespawnPos = position;

        AudioManager.instance.Play("Checkpoint");
    }


    public void Restart()
    {
        GameObject.Find("Fade").GetComponent<Animator>().Play("FadeOut");

        Invoke("Load", 2);
    }

    public void Load()
    {
        SceneManager.LoadScene("Main Scene");

    }
}
