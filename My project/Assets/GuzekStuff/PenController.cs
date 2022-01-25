using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PenController : MonoBehaviour
{
    // Start is called before the first frame update

    public static PenController Instance;

    public float PenMagnitude = 0.1f;
    public float PenPressure = 0.1f;

    public float CheckTime = 0.2f;

    float width;
    float height;
    float halfWidth;

    public bool IsChecking = false;

    public bool SwipedLeft = false;
    public bool SwipedRight = false;

    public bool GotInput = false;


    void Start()
    {
        Instance = this;
        width = Screen.width;
        height = Screen.height;

        halfWidth = width / 2;
    }


    IEnumerator _swipeCheck;

    IEnumerator SwipeCheck()
    {
        IsChecking = true;
        bool side = CheckSide(Pen.current.position.ReadValue());
        while(Pen.current.pressure.ReadValue() > PenPressure)
        {
            yield return new WaitForEndOfFrame();
        }
        bool side2 = CheckSide(Pen.current.position.ReadValue());

        // From left to right
        if (side == false && side != side2)
            SwipedRight = true;
        else if (side == true && side != side2)
            SwipedLeft = true;
        else
            Debug.Log("No swipe");

        GotInput = true;
        IsChecking = false;
        yield break;
    }


    // Update is called once per frame
    void Update()
    {
        if (Pen.current != null && Pen.current.pressure.ReadValue() > PenPressure && !IsChecking && !GotInput && PadController.Instance.Attacked)
        {
            _swipeCheck = SwipeCheck();
            StartCoroutine(_swipeCheck);
        }
            
    }

    /// <summary>
    /// Checks on which side of screen is pointer
    /// </summary>
    /// <param name="position">Position of pointer</param>
    /// <returns>Side of the pointer - false (0) equals left, true (1) equals rights</returns>
    bool CheckSide(Vector3 position)
    {
        if (position.x > halfWidth)
            return true;
        else
            return false;
    }

    public void ResetPenInput()
    {
        SwipedLeft = false;
        SwipedRight = false;
        GotInput = false;
    }

}
