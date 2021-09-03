using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterXYInputBehaviour : MonoBehaviour
{

    private CharacterController _myCharacterController;
    private Vector3 _moveDirection;

    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
    }
    
    void LateUpdate()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            _moveDirection.Set(Mathf.RoundToInt(Input.GetAxis("Horizontal")), Mathf.RoundToInt(Input.GetAxis("Vertical")), 0);
            _moveDirection = transform.TransformDirection(_moveDirection);
            _myCharacterController.Move(_moveDirection);
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), 
                                             Mathf.RoundToInt(transform.position.y), 
                                             Mathf.RoundToInt(transform.position.z));
        }
    }
}
