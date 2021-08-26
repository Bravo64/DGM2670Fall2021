using System.Collections;
using UnityEngine;

public class CharacterMoveXBehaviour : MonoBehaviour
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
        PlayControllerXMovement();
    }
    
    public void PlayControllerXMovement()
    {
        if (directionType == DirectionTypes.Global)
        {
            StartCoroutine(ConstMoveGlobalX());
        }
        else if (directionType == DirectionTypes.Local)
        {
            StartCoroutine(ConstMoveLocalX());
        }
    }
    
    public void SetSpeedVariableValue(float inputSpeed)
    {
        speedVariableValue = inputSpeed;
    }

    IEnumerator ConstMoveGlobalX()
    {
        while (true)
        {
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _myCharacterController.Move(Vector3.right * (speedVariableValue * Time.deltaTime));
            yield return 0;
        }
    }
    
    IEnumerator ConstMoveLocalX()
    {
        while (true)
        {
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _myCharacterController.Move(transform.right * (speedVariableValue * Time.deltaTime));
            yield return 0;
        }
    }

    private void PauseControllerXMovement()
    {
        StopAllCoroutines();
    }
    
    private void OnDisable()
    {
        PauseControllerXMovement();
    }
}
