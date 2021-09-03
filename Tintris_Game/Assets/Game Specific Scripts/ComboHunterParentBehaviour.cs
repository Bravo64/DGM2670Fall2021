using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ComboHunterParentBehaviour : MonoBehaviour
{
    public ComboHunterBehaviour[] comboHunterChildren;
    public IntData somethingIsFalling;
    public UnityEvent shapeCreationAcceptedEvent;

    public void ActivateComboHunterChild(GameObject inputGameObject)
    {
        foreach (ComboHunterBehaviour comboHunter in comboHunterChildren)
        {
            if (!comboHunter.gameObject.activeInHierarchy)
            {
                comboHunter.gameObject.SetActive(true);
                comboHunter.ActivateComboHunter(inputGameObject);
                break;
            }
        }
    }

    public void CreateNewShapeWasCalled()
    {
        StartCoroutine(CheckAndWait());
    }

    IEnumerator CheckAndWait()
    {
        yield return 5;
        foreach (ComboHunterBehaviour comboHunter in comboHunterChildren)
        {
            while (comboHunter.gameObject.activeSelf)
            {
                yield return 0;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            while (somethingIsFalling.value == 1)
            {
                yield return 0;
            }
            yield return 0;
        }
        shapeCreationAcceptedEvent.Invoke();
    }
}
