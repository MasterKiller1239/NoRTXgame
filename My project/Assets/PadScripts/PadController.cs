using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PadController : MonoBehaviour
{
    int right = 1;
    public float period = 0.0f;
    int currentpos = 1;
    public int i = 0;
    bool isShowed=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (period >5)
        {
            if(!isShowed)
            {
                Debug.Log("Punch");
                isShowed = true;
                currentpos = Random.Range(0, 100)%2;
                Debug.Log(currentpos);
            }

            if (Gamepad.current.xButton.IsPressed())
            {
                right = 0;
                if (right == currentpos)
                {
                    Debug.Log("Hit");
                    i--;
                }
                else Debug.Log("Miss");
                period = 0;
                isShowed = false;
            }
              
            else if(Gamepad.current.bButton.IsPressed()||period>10)
            {
                right = 1;
                if (right == currentpos)
                {
                    i--;
                    Debug.Log("Hit");
                }
                else Debug.Log("Miss");
                period = 0;
                isShowed = false;
            }

        }
        period += UnityEngine.Time.deltaTime;
       


}
}