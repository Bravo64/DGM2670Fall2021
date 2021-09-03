using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CharacterMoveZBehaviour : MonoBehaviour
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
        PlayControllerZMovement();
    }
    
    public void PlayControllerZMovement()
    {
        if (directionType == DirectionTypes.Global)
        {
            StartCoroutine(ConstMoveGlobalZ());
        }
        else if (directionType == DirectionTypes.Local)
        {
            StartCoroutine(ConstMoveLocalZ());
        }
    }
    
    public void SetSpeedVariableValue(float inputSpeed)
    {
        speedVariableValue = inputSpeed;
    }

    IEnumerator ConstMoveGlobalZ()
    {
        while (true)
        {
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _myCharacterController.Move(Vector3.forward * (speedVariableValue * Time.deltaTime));
            yield return 0;
        }
    }

    IEnumerator ConstMoveLocalZ()
    {
        while (true)
        {
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _myCharacterController.Move(transform.forward * (speedVariableValue * Time.deltaTime));
            yield return 0;
        }
    }

    private void PauseControllerZMovement()
    {
        StopAllCoroutines();
    }
    
    private void OnDisable()
    {
        PauseControllerZMovement();
    }
}
