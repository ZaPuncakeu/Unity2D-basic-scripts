using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    private bool isKeyBoard = true;

    [SerializeField]
    private string[] inputNames = new string[4];

    [SerializeField]
    private KeyCode[] keyboard = new KeyCode[4];

    [SerializeField]
    private KeyCode[] controller = new KeyCode[4];

    public int GetAxis(string axis)
    {
        if(axis == "Horizontal")
        {
            if(isKeyBoard)
            {
                if(Input.GetKey(keyboard[0]))
                {
                    return 1;
                }

                if(Input.GetKey(keyboard[1]))
                {
                    return -1;
                }

                return 0;
            }
            else 
            {
                if(Input.GetKey(controller[0]))
                {
                    return 1;
                }

                if(Input.GetKey(controller[1]))
                {
                    return -1;
                }

                return 0;
            }
        }

        return 0;
    }

    public bool GetButtonDown(int index)
    {
        if(isKeyBoard)
        {
            return Input.GetKeyDown(keyboard[index]);
        }
        else 
        {
            return Input.GetKeyDown(controller[index]);
        }

        return false;
    }
}

/* 
    0 -> right
    1 -> left
    2 -> jump
*/
