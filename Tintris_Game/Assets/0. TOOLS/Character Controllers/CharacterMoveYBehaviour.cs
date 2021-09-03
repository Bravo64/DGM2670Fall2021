using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CharacterMoveYBehaviour : MonoBehaviour
{
    public enum Modes { UseMyCharacterController, UseParentCharacterController }
    public enum DirectionTypes { Global, Local}
    public enum SpeedTypes { UseSpeedVariableValue, UseFloatDataSpeed }

    public Modes controller = Modes.UseMyCharacterController;
    public SpeedTypes speedType = SpeedTypes.UseSpeedVariableValue;
    public float speedVariableValue;
    public FloatData floatDataSpeed;
    public DirectionTypes directionType = DirectionTypes.Global;
    
    private CharacterController _myCharacterController;

    void Start()
    {
        if (controller == Modes.UseMyCharacterController)
        {
            _myCharacterController = GetComponent<CharacterController>();
        }
        else
        {
            _myCharacterController = transform.parent.GetComponent<CharacterController>();
        }
        PlayControllerYMovement();
    }
    
    public void PlayControllerYMovement()
    {
        if (directionType == DirectionTypes.Global)
        {
            StartCoroutine(ConstMoveGlobalY());
        }
        else if (directionType == DirectionTypes.Local)
        {
            StartCoroutine(ConstMoveLocalY());
        }
    }
    
    public void SetSpeedVariableValue(float inputSpeed)
    {
        speedVariableValue = inputSpeed;
    }

    IEnumerator ConstMoveGlobalY()
    {
        while (true)
        {
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _myCharacterController.Move(Vector3.up * (speedVariableValue * Time.deltaTime));
            yield return 0;
        }
    }

    IEnumerator ConstMoveLocalY()
    {
        while (true)
        {
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _myCharacterController.Move(transform.up * (speedVariableValue * Time.deltaTime));
            yield return 0;
        }
    }

    private void PauseControllerYMovement()
    {
        StopAllCoroutines();
    }
    
    private void OnDisable()
    {
        PauseControllerYMovement();
    }
}
