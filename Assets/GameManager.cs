using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject plant;

    private Vector2 _playerRespawnPos;
    private Vector2 _plantRespawnPos;
    [SerializeField] private Vector2 _startPlayerRespawnPos;
    [SerializeField] private Vector2 _startPlantRespawnPos;

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

        _playerRespawnPos = _startPlayerRespawnPos;
        _plantRespawnPos = _startPlantRespawnPos;
    }




    public void Checkpoint(Vector2 position)
    {
        _playerRespawnPos = position;
        _plantRespawnPos = position;
    }


    public void Restart()
    {
        print("death");

        player.transform.position = _playerRespawnPos;
        plant.transform.position = _plantRespawnPos;
    }
}
