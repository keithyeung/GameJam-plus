using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject plant;

    private Vector2 _playerRespawnPos;
    private Vector2 _plantRespawnPos;

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
        print("death");

        player.transform.position = _playerRespawnPos;
        plant.transform.position = _plantRespawnPos;
    }
}
