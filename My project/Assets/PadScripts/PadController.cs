using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PadController : MonoBehaviour
{
    public static PadController Instance;

    int right = 1;
    public float period = 0.0f;
    int currentpos = 1;
    public int i = 0;

    public float EnemyLogicCooldown = 5.0f;
    public bool XPressed = false;
    public bool BPressed = false;
    public bool GotInput = false;
    public bool Attacked = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        _enemyLogic = EnemyLogic();
        StartCoroutine(_enemyLogic);
    }

    IEnumerator _enemyLogic;
    IEnumerator EnemyLogic()
    {
        for (; ; )
        {
            Debug.Log("Waiting for attack");
            yield return new WaitForSecondsRealtime(EnemyLogicCooldown);

            
            currentpos = Random.Range(0, 2);
            Debug.Log($"Punch, currentPos: {currentpos}");
            Attacked = true;

            while (!GotInput && !PenController.Instance.GotInput && period < EnemyLogicCooldown)
            {
                period += UnityEngine.Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (GotInput || PenController.Instance.GotInput)
            {
                if (currentpos == 0 && (XPressed || PenController.Instance.SwipedLeft))
                {
                    Debug.Log("Hit");
                    i--;
                    period = 0;
                }
                else if (currentpos == 1 && (BPressed || PenController.Instance.SwipedRight))
                {
                    Debug.Log("Hit");
                    i--;
                    period = 0;
                }
                else
                {
                    Debug.Log("Wrong input");
                    Debug.Log("Miss");
                    period = 0;
                }                
            }
            else
            {
                Debug.Log("Not enough time");
                Debug.Log("Miss");
                period = 0;
            }

            Attacked = false;

            ResetPadInput();
            PenController.Instance.ResetPenInput();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.xButton.IsPressed() && !GotInput && Attacked)
        {
            XPressed = true;
            GotInput = true;
        }           

        if (Gamepad.current.bButton.IsPressed() && !GotInput && Attacked)
        {
            BPressed = true;
            GotInput = true;
        }       
    }

    public void ResetPadInput()
    {
        XPressed = false;
        BPressed = false;
        GotInput = false;
    }
}