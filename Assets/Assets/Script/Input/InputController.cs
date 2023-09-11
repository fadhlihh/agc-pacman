using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Action<Vector3> OnMove;

    private void Update()
    {
        DetectMoveInput();
    }

    private void DetectMoveInput()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 axisDirection = new Vector3(horizontalAxis, 0, verticalAxis);

        if (OnMove != null)
        {
            OnMove(axisDirection);
        }
    }
}
