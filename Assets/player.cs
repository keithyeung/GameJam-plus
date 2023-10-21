using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //pick ups
    private List<GameObject> _pickableObjects = new List<GameObject>();
    private GameObject _heldObject;
    [SerializeField] private Transform _holdingPoint;

    //interactions
    private GameObject _interactableObject;

    //throwing
    [SerializeField] private GameObject _aim;
    [SerializeField] private Transform _aimCircle;
    [SerializeField] private float _aimRotaionSpeed;
    private Vector3 _aimRotation;
    private int _rotateDir = 1;
    [SerializeField] private float _throwSpeed;

    bool _lookingRight;

    // Update is called once per frame
    void Update()
    {
        //fake movement
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * x * 10 * Time.deltaTime);

        if (x == 1)
        {
            _lookingRight = true;
        }
        else if (x == -1)
        {
            _lookingRight = false;
        }

        PickUp();
        Interact();
        Throw();
    }


    private void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //pickup
            if (_heldObject == null)
            {
                if (_pickableObjects.Count > 0)
                {
                    _heldObject = _pickableObjects[0];
                    _heldObject.transform.SetParent(_holdingPoint);
                    _heldObject.transform.localPosition = Vector3.zero;
                    _heldObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    _heldObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

                    AudioManager.instance.Play("PickUp");
                }
            }
            //drop
            else
            {
                _heldObject.transform.SetParent(null);
                _heldObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                _heldObject = null;
            }

        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_interactableObject)
            {
                _interactableObject.GetComponent<Interactable>().Interact();
            }
        }
    }


    private void Throw()
    {
        //start
        if (_heldObject)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _aim.SetActive(true);
                _aimCircle.rotation = Quaternion.Euler(Vector3.zero);
                _aimRotation = Vector3.zero;
            }
            else if (Input.GetKey(KeyCode.R))
            {
                _aimRotation = new Vector3(0, 0, _aimRotation.z + _aimRotaionSpeed * Time.deltaTime * _rotateDir);

                //up or down
                if (_rotateDir == 1)
                {
                    if (_aimRotation.z >= 90)
                    {
                        _rotateDir = -1;
                    }
                }
                else if (_rotateDir == -1)
                {
                    if (_aimRotation.z <= 0)
                    {
                        _rotateDir = 1;
                    }
                }

                //direction
                float dir = (_lookingRight) ? -1 : 1;
                Vector3 rot = _aimRotation * dir;

                //do it
                _aimCircle.rotation = Quaternion.Euler(rot);
            }
            else if (Input.GetKeyUp(KeyCode.R))
            {
                float dir = (_lookingRight) ? 1 : -1;

                _heldObject.transform.SetParent(null);
                _heldObject.GetComponent<Rigidbody2D>().gravityScale = 1;

                Vector2 throwDir = new Vector2(Mathf.Sin(_aimRotation.z * Mathf.Deg2Rad) * dir, Mathf.Cos(_aimRotation.z * Mathf.Deg2Rad));
                
                _heldObject.GetComponent<Rigidbody2D>().velocity = throwDir * _throwSpeed;
                _heldObject = null;

                _aim.SetActive(false);
            }
        }
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //pick up
        if (collision.tag == "PickUp")
        {
            _pickableObjects.Add(collision.transform.parent.gameObject);
        }
        //interactive
        else if (collision.tag == "Interactive")
        {
            _interactableObject = collision.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //pick up
        if (_pickableObjects.Contains(collision.transform.parent.gameObject))
        {
            _pickableObjects.Remove(collision.transform.parent.gameObject);
        }
        //interactive
        else if (_interactableObject == collision.transform.parent.gameObject)
        {
            _interactableObject = null;
        }
    }
}
