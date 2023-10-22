using UnityEngine;

public class WormGround : MonoBehaviour
{

    [SerializeField] private GameObject _worm;
    [SerializeField] private float _wormChillSpeed;
    [SerializeField] private float _wormCuriosSpeed;
    [SerializeField] private float _wormAttackSpeed;
    private float _wormCurve = 0;

    private bool _playerOn = false;
    private bool _plantOn = false;

    enum state { chilling, curious, attacking, inAir}
    private state _state = state.chilling;

    private float _attackTimer = 0;
    [SerializeField] private float _attackDuration;
    private float _curiosTimer = 0;
    [SerializeField] private float _curiosDuration;

    [SerializeField] private Animator _anim;


    private Transform _plant;


    private void Start()
    {
        _state = state.chilling;
        _attackTimer = 0;
        _wormCurve = Mathf.Asin(_worm.transform.localPosition.x * 2.5f);
    }


    // Update is called once per frame
    void Update()
    {
        if (_worm.activeSelf)
        {
            //moving
            if (_state == state.chilling)
            {
                _wormCurve += Time.deltaTime * _wormChillSpeed;

                float sine = Mathf.Sin(_wormCurve);
                if (sine / 2.5f == float.NaN)
                {
                    print("sine value became NaN, it's got the value " + sine);
                    return;
                }

                _worm.transform.localPosition = new Vector3(sine / 2.5f, 0, 0);


                //attack
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= _attackDuration)
                {
                    _anim.Play("WormAnim");
                    _state = state.inAir;
                }
            }
            else if (_state == state.curious)
            {
                Vector2 targetPos = new Vector2(GameManager.instance.player.transform.position.x, _worm.transform.position.y);
                _worm.transform.position = Vector2.MoveTowards(_worm.transform.position, targetPos, Time.deltaTime * _wormCuriosSpeed);

                if (Mathf.Abs(_worm.transform.position.x - targetPos.x) < 0.05)
                {
                    _curiosTimer += Time.deltaTime;
                    if (_curiosTimer >= _curiosDuration)
                    {
                        _anim.Play("WormAnim");
                        _state = state.inAir;
                    }
                }
            }
            else if (_state == state.attacking)
            {
                Vector2 targetPos = new Vector2(_plant.position.x, _worm.transform.position.y);
                _worm.transform.position = Vector2.MoveTowards(_worm.transform.position, targetPos, Time.deltaTime * _wormAttackSpeed);

                if (Mathf.Abs(_worm.transform.position.x - targetPos.x) < 0.05)
                {
                    _anim.Play("WormAnim");
                    _state = state.inAir;
                }
            }
            else if (_state == state.inAir)
            {
                //wait
            }
        }
        

        

        
    }


    public void BackInGround()
    {
        if (_plantOn)
        {
            _state = state.attacking;
        }
        else if (_playerOn)
        {
            _state = state.curious;
            _curiosTimer = 0;
        }
        else
        {
            _state = state.chilling;
            _attackTimer = 0;
            _wormCurve = Mathf.Asin(_worm.transform.localPosition.x * 2.5f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = true;

            if (_state != state.inAir)
            {
                if (!_plantOn)
                {
                    _state = state.curious;
                    _curiosTimer = 0;
                }
            }
                
        }
        else if (collision.name.Contains("Plant"))
        {
            _plantOn = true;

            _plant = collision.gameObject.transform;

            if (_state != state.inAir)
                _state = state.attacking;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerOn = false;

            if (_state != state.inAir)
            {
                if (!_plantOn)
                {
                    _state = state.chilling;
                    _attackTimer = 0;
                    _wormCurve = Mathf.Asin(_worm.transform.localPosition.x * 2.5f);
                }
            }
                
        }
        else if (collision.name.Contains("Plant"))
        {
            _plantOn = false;

            _plant = null;

            if (_state != state.inAir)
            {
                if (_playerOn)
                {
                    _state = state.curious;
                    _curiosTimer = 0;
                }
                else
                {
                    _state = state.chilling;
                    _attackTimer = 0;
                    _wormCurve = Mathf.Asin(_worm.transform.localPosition.x * 2.5f);
                }
            }
        }
    }


}
