using System;
using System.Collections;
using System.Collections.Generic;
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


    public void Grow()
    {
        _rend.sprite = _levels[_levelIndex].sprite;
        _light.intensity = _levels[_levelIndex].lightIntensity;

        _levelIndex++;

        print("Grow");
    }

}
