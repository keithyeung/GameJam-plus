using UnityEngine;

public class LibraScale : MonoBehaviour
{

    [SerializeField] private Transform platform1;
    [SerializeField] private Transform ropeMask1;
    [SerializeField] private Vector3 highPos1;
    [SerializeField] private Vector3 lowPos1;
    [SerializeField] private Transform platform2;
    [SerializeField] private Transform ropeMask2;
    [SerializeField] private Vector3 highPos2;
    [SerializeField] private Vector3 lowPos2;

    private bool _moving = false;
    private int _dir = 0;

    [SerializeField] private float _speed;


    // Update is called once per frame
    void Update()
    {
        if (_moving)
        {
            if (_dir < 0)
            {
                platform1.position = Vector2.MoveTowards(platform1.position, highPos1, Time.deltaTime * _speed * Mathf.Abs(_dir));
                platform2.position = Vector2.MoveTowards(platform2.position, lowPos2, Time.deltaTime * _speed * Mathf.Abs(_dir));

                float r = ((platform1.position.y-lowPos1.y)/(highPos1.y - lowPos1.y));
                ropeMask1.localPosition = new Vector3(0, r ,0);
                r = ((platform2.position.y - lowPos2.y) / (highPos2.y - lowPos2.y));
                ropeMask2.localPosition = new Vector3(0, r, 0);

                if (platform1.position == highPos1 && platform2.position == lowPos2)
                {
                    _moving = false;
                }
            }
            else if (_dir > 0)
            {
                platform1.position = Vector2.MoveTowards(platform1.position, lowPos1, Time.deltaTime * _speed * _dir);
                platform2.position = Vector2.MoveTowards(platform2.position, highPos2, Time.deltaTime * _speed * _dir);

                float r = ((platform1.position.y - lowPos1.y) / (highPos1.y - lowPos1.y));
                ropeMask1.localPosition = new Vector3(0, r, 0);
                r = ((platform2.position.y - lowPos2.y) / (highPos2.y - lowPos2.y));
                ropeMask2.localPosition = new Vector3(0, r, 0);

                if (platform1.position == lowPos1 && platform2.position == highPos2)
                {
                    _moving = false;
                }
            }
        }
        

    }

    public void AddObject(int side)
    {        
        if (_dir == 0)
        {
            _moving = true;
        }

        _dir += side;

        if (_dir == 0)
        {
            _moving = false;
        }
    }

    public void RemoveObject(int side)
    {
        if (_dir == 0)
        {
            _moving = true;
        }

        _dir -= side;

        if (_dir == 0)
        {
            _moving = false;
        }
    }



}
