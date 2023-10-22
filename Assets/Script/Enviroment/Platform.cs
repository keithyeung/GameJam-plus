using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private Vector2 _endPosition;
    private Vector3 _goalPosition;
    [SerializeField] private bool _keepMoving;
    private bool _isMoving;
    private bool _isMovingTowardsEnd;
    [SerializeField] private float _speed;

    public void StartMoving()
    {
        _isMoving = true;
        _goalPosition = _endPosition;
        _isMovingTowardsEnd = true;

        AudioManager.instance.Play("PlatformMoving");
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, _goalPosition, Time.deltaTime * _speed);
            
            if (transform.localPosition == _goalPosition)
            {
                if (_keepMoving)
                {
                    _isMovingTowardsEnd = !_isMovingTowardsEnd;

                    _goalPosition = (_isMovingTowardsEnd) ? _endPosition : _startPosition;
                }
                else
                {
                    _isMoving = false;
                    AudioManager.instance.Stop("PlatformMoving");
                }
            }
        }
    }






}
