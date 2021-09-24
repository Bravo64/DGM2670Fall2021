using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 
Color to Number Correlations:

0 —> Red
1 —> Yellow
2 —> Blue
3 —> Green
4 —> Purple
5 —> Orange

*/

public class DotBehaviour : MonoBehaviour
{
    public bool runOnStart = true;
    public SpriteRenderer colorSpriteRenderer;
    public Transform[] comboConnectorPivots;
    public SpriteRenderer[] comboConnectorSprites;
    public GameObject comboSensor, groundSensor, errorSensor;
    public GameObjectEvent activateComboHunter, colorAppliedEvent;
    public IntData totalNumberOfColors, somethingIsFallingObj;
    public bool badDot = false;

    [HideInInspector] public List<GameObject> potentialCombos = new List<GameObject>();
    [HideInInspector] public bool _gravityActivated = false;
    private int _randomIndex;
    private bool _creationPeriod = true, _disabledConnectors;
    [HideInInspector] public Rigidbody2D myRigidbody2D;

    private Color[] _colors = new Color[]
    {
        new Color32( 255 , 100 , 100, 255 ),
        new Color32( 255 , 255 , 100, 255 ),
        new Color32( 0 , 150 , 255, 255 ),
        new Color32( 0 , 255 , 150, 255 ),
        new Color32( 175 , 125 , 255, 255 ),
        new Color32( 255 , 150 , 25, 255 ),
    };

    private string[] _colorNames = new string[]
    {
        "Red",
        "Yellow",
        "Blue",
        "Green", 
        "Purple",
        "Orange"
    };

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        if (runOnStart)
        {
            StartCoroutine(CreationPeriodDuration());
            AssignRandomColor();
        }
    }

    private void Update()
    {
        if (!_gravityActivated) { return; }
        
        foreach (Transform connector in comboConnectorPivots)
        {
            if (connector.gameObject.activeSelf)
            {
                connector.gameObject.SetActive(false);
            }
        }
        if (potentialCombos.Count > 0)
        {
            _disabledConnectors = false;
            int maxLength;
            if (potentialCombos.Count < 4)
            {
                maxLength = potentialCombos.Count;
            }
            else
            {
                maxLength = 4;
            }
            for (int i = 0; i < maxLength; i++)
            {
                if (potentialCombos[i].activeSelf)
                {
                    if (Vector2.Distance(comboConnectorPivots[i].transform.position, potentialCombos[i].transform.position) < 1.1f)
                    {
                        comboConnectorPivots[i].gameObject.SetActive(true);
                        Vector3 lookPos = potentialCombos[i].transform.position;
                        lookPos.Set(Mathf.FloorToInt(lookPos.x) + 0.5f, Mathf.FloorToInt(lookPos.y) + 0.5f, 0);
                        comboConnectorPivots[i].LookAt(lookPos);
                    }
                }
                else
                {
                    comboConnectorPivots[i].gameObject.SetActive(false);
                }
            }
        }
        else if (!_disabledConnectors)
        {
            foreach (Transform connector in comboConnectorPivots)
            {
                connector.gameObject.SetActive(false);
            }
            _disabledConnectors = true;
        }
    }

    IEnumerator CreationPeriodDuration()
    {
        yield return new WaitForSeconds(0.1f);
        _creationPeriod = false;
    }
    
    public void ShapeLandedEvent()
    {
        comboSensor.SetActive(true);
        groundSensor.SetActive(true);
        if (!gameObject.activeInHierarchy) { return;}
        if (!_gravityActivated)
        {
            myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            myRigidbody2D.AddForce(Vector2.down * 100);
            _gravityActivated = true;
            StartCoroutine(WaitTwoFrames());
        }
        StartCoroutine(WaitAndCall());
    }

    IEnumerator WaitTwoFrames()
    {
        yield return 1;
        if (errorSensor != null)
        {
            errorSensor.SetActive(true);
        }
    }

    IEnumerator WaitAndCall()
    {
        while (myRigidbody2D.bodyType == RigidbodyType2D.Dynamic) { yield return 0; }
        if (!gameObject.activeInHierarchy) { yield break;}
        yield return Random.Range(5, 10);
        CheckPotentialCombos();
    }

    private void CheckPotentialCombos()
    {
        if (potentialCombos.Count > 0)
        {
            foreach (GameObject comboDot in potentialCombos)
            {
                Rigidbody2D dotRigidbody = comboDot.GetComponent<Rigidbody2D>();
                if (dotRigidbody.bodyType == RigidbodyType2D.Dynamic || myRigidbody2D.bodyType == RigidbodyType2D.Dynamic)
                {
                    ShapeLandedEvent();
                    return;
                }
            }
            StartCoroutine(WaitAndActivate());
        }
    }

    IEnumerator WaitAndActivate()
    {
        for (int i = 0; i < 3; i++)
        {
            while (somethingIsFallingObj.value == 1)
            {
                yield return 0;
            }
            yield return 0;
        }
        activateComboHunter.Raise(gameObject);
    }

    public void AssignRandomColor()
    {
        _randomIndex = Random.Range(0, totalNumberOfColors.value);
        foreach (var connector in comboConnectorSprites)
        {
            connector.color = _colors[_randomIndex];
        }
        colorSpriteRenderer.color = _colors[_randomIndex];
        string colorTag = _colorNames[_randomIndex];
        colorSpriteRenderer.gameObject.tag = gameObject.tag = comboSensor.tag = colorTag;
        colorAppliedEvent.Raise(gameObject);
    }

    public void ColorAlreadyTaken(GameObject inputGameObject)
    {
        if (!_creationPeriod) { return;}
        if (inputGameObject == gameObject) { return;}
        StartCoroutine(Wait(inputGameObject));
    }

    IEnumerator Wait(GameObject inputGameObject)
    {
        yield return Random.Range(0, 2);
        if (inputGameObject.CompareTag(gameObject.tag))
        {
            AssignRandomColor();
        }
    }
}

/*
 
Color to Number Correlations:

0 —> Red
1 —> Yellow
2 —> Blue
3 —> Green
4 —> Purple
5 —> Orange

*/
