using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PadController : MonoBehaviour
{
    public static PadController Instance;

    public bool XPressed = false;
    public bool BPressed = false;
    public bool GotInput = false;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    IEnumerator _enemyLogic;
    

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.xButton.IsPressed() && !GotInput && EnemyController.Instance.Attacked)
        {
            XPressed = true;
            GotInput = true;
        }           

        if (Gamepad.current.bButton.IsPressed() && !GotInput && EnemyController.Instance.Attacked)
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