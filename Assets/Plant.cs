using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Plant : MonoBehaviour
{
    [Serializable]
    public struct GrowthLevel
    {
        public Sprite sprite;
        public float lightRadius;
        public float lightIntensity;
    }
    [SerializeField] private GrowthLevel[] _levels;
    private int _levelIndex = 0;

    [SerializeField] private SpriteRenderer _rend;
    [SerializeField] private Light2D _light;


    private void Start()
    {
        _rend.sprite = _levels[_levelIndex].sprite;
        _light.intensity = _levels[_levelIndex].lightIntensity;
    }

    public void Grow()
    {
        _levelIndex++;
        _rend.sprite = _levels[_levelIndex].sprite;
        _light.intensity = _levels[_levelIndex].lightIntensity;

        AudioManager.instance.Play("NewHead");
        AudioManager.instance.voice++;
        AudioManager.instance.Play("VoiceFirstTime" + AudioManager.instance.voice);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Worm")
        {
            if (name == "Plant")
            {
                AudioManager.instance.Play("PlantDeath");
                GameManager.instance.Restart();
            }
            else
            {
                gameObject.SetActive(false);

                AudioManager.instance.Stop("SongMain");
                AudioManager.instance.Play("SongDrums");
            }
        }
    }
}
