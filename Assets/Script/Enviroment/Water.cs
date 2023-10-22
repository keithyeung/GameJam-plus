using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private bool no;
    public int growth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Plant")
        {
            AudioManager.instance.Play("Splash");

            if (!no)
            {
                GameManager.instance.Checkpoint(_spawnPoint.position);
                collision.GetComponent<Plant>().Grow(growth);
                no = true;
            }

        }
    }
}
