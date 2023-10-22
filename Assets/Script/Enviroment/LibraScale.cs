using UnityEngine;

public class LibraScale : MonoBehaviour
{

    [SerializeField] private Transform platform1;
    [SerializeField] private Transform ropeMask1;
    [SerializeField] private Vector3 highPos1;
    [SerializeField] private Vector3 middlePos1;
    [SerializeField] private Vector3 lowPos1;
    [SerializeField] private Transform platform2;
    [SerializeField] private Transform ropeMask2;
    [SerializeField] private Vector3 highPos2;
    [SerializeField] private Vector3 middlePos2;
    [SerializeField] private Vector3 lowPos2;

    private int _dir = 0;

    [SerializeField] private float _speed;

    public enum scaleState { one, middle, two, moving}
    private scaleState _state = scaleState.moving;

    // Update is called once per frame
    void Update()
    {
        if (_state == scaleState.moving)
        {
            if (_dir < 0)
            {
                platform1.localPosition = Vector2.MoveTowards(platform1.localPosition, highPos1, Time.deltaTime * _speed * Mathf.Abs(_dir));
                platform2.localPosition = Vector2.MoveTowards(platform2.localPosition, lowPos2, Time.deltaTime * _speed * Mathf.Abs(_dir));

                float r = ((platform1.localPosition.y-lowPos1.y)/(highPos1.y - lowPos1.y));
                ropeMask1.localPosition = new Vector3(0, r ,0);
                r = ((platform2.localPosition.y - lowPos2.y) / (highPos2.y - lowPos2.y));
                ropeMask2.localPosition = new Vector3(0, r, 0);

                if (platform1.localPosition == highPos1 && platform2.localPosition == lowPos2)
                {
                    _state = scaleState.one;
                }
            }
            else if (_dir > 0)
            {
                platform1.localPosition = Vector2.MoveTowards(platform1.localPosition, lowPos1, Time.deltaTime * _speed * _dir);
                platform2.localPosition = Vector2.MoveTowards(platform2.localPosition, highPos2, Time.deltaTime * _speed * _dir);

                float r = ((platform1.localPosition.y - lowPos1.y) / (highPos1.y - lowPos1.y));
                ropeMask1.localPosition = new Vector3(0, r, 0);
                r = ((platform2.localPosition.y - lowPos2.y) / (highPos2.y - lowPos2.y));
                ropeMask2.localPosition = new Vector3(0, r, 0);

                if (platform1.localPosition == lowPos1 && platform2.localPosition == highPos2)
                {
                    _state = scaleState.middle;
                }
            }
            else if (_dir == 0)
            {
                platform1.localPosition = Vector2.MoveTowards(platform1.localPosition, middlePos1, Time.deltaTime * _speed);
                platform2.localPosition = Vector2.MoveTowards(platform2.localPosition, middlePos2, Time.deltaTime * _speed);

                float r = ((platform1.localPosition.y - lowPos1.y) / (highPos1.y - lowPos1.y));
                ropeMask1.localPosition = new Vector3(0, r, 0);
                r = ((platform2.localPosition.y - lowPos2.y) / (highPos2.y - lowPos2.y));
                ropeMask2.localPosition = new Vector3(0, r, 0);

                if (platform1.localPosition == middlePos1 && platform2.localPosition == middlePos2)
                {
                    _state = scaleState.two;
                }
            }

        }
        

    }

    public void AddObject(int side)
    {        
        _dir += side;

        _state = scaleState.moving;
    }

    public void RemoveObject(int side)
    {
        _dir -= side;

        _state = scaleState.moving;
    }



}
