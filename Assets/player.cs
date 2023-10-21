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

    // Update is called once per frame
    void Update()
    {
        //fake movement
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * x * 10 * Time.deltaTime);

        PickUp();
        Interact();
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
