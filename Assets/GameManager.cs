using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public GameObject player;

    private Vector2 _respawnPos;

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
    }


    public void Checkpoint(Vector2 position)
    {
        _respawnPos = position;
    }


    public void Restart()
    {
        print("death");

        player.transform.position = _respawnPos;
    }
}
