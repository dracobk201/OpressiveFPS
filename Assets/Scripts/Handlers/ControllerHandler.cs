using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerHandler : MonoBehaviour
{
    #region Directional buttons
    [Header("Directional Buttons Variables")]
    [SerializeField] private BoolReference horizontalSinglePress;
    [SerializeField] private FloatReference horizontalAxis;
    [SerializeField] private GameEvent nonHorizontalAxisEvent;
    [SerializeField] private GameEvent leftButtonEvent;
    [SerializeField] private GameEvent rightButtonEvent;
    private bool _isHorizontalAxisInUse;

    [SerializeField] private BoolReference verticalSinglePress;
    [SerializeField] private FloatReference verticalAxis;
    [SerializeField] private GameEvent upButtonEvent;
    [SerializeField] private GameEvent downButtonEvent;
    [SerializeField] private GameEvent nonVerticalAxisEvent;
    private bool _isVerticalAxisInUse;

    //[SerializeField] private GameEvent noDirectionalAxisEvent;

    #endregion

    #region Action Buttons
    [Header("Action Buttons Variables")]
    [SerializeField] private GameEvent startButtonEvent;
    [SerializeField] private GameEvent squareButtonEvent;
    [SerializeField] private GameEvent xButtonEvent;

    private bool isStartAxisInUse = false;
    private bool isSquareAxisInUse = false;
    private bool isXAxisInUse = false;
    #endregion
    
    [Header("UI Active Variables")]
    [SerializeField] private BoolReference uiPanelActive;
    [SerializeField] private GameEvent uiChangeEvent;

    private void Update()
    {
        CheckingVerticalAxis();
        CheckingHorizontalAxis();
        CheckingStartButton();
        CheckingSquareButton();
        CheckingXButton();
    }

    #region Horizontal Functions

    private void CheckingHorizontalAxis()
    {
        if (Input.GetAxisRaw(Global.HorizontalAxis) < 0 && !_isHorizontalAxisInUse)
            LeftDirectionActions();
        else if (Input.GetAxisRaw(Global.HorizontalAxis) > 0 && !_isHorizontalAxisInUse)
            RightDirectionActions();
        else if (Math.Abs(Input.GetAxisRaw(Global.HorizontalAxis)) < Global.Tolerance)
            NoHorizontalActions();
    }

    private void NoHorizontalActions()
    {
        horizontalAxis.Value = 0;
        if (horizontalSinglePress.Value)
            _isHorizontalAxisInUse = false;
        nonHorizontalAxisEvent.Raise();
    }

    private void RightDirectionActions()
    {
        horizontalAxis.Value = 1;
        if (horizontalSinglePress.Value)
            _isHorizontalAxisInUse = true;
        rightButtonEvent.Raise();
    }

    private void LeftDirectionActions()
    {
        horizontalAxis.Value = -1;
        if (horizontalSinglePress.Value)
            _isHorizontalAxisInUse = true;
        leftButtonEvent.Raise();
    }

    #endregion

    #region Vertical Functions
    private void CheckingVerticalAxis()
    {
        if (Input.GetAxisRaw(Global.VerticalAxis) < 0 && !_isVerticalAxisInUse)
            DownDirectionActions();
        else if (Input.GetAxisRaw(Global.VerticalAxis) > 0 && !_isVerticalAxisInUse)
            UpDirectionActions();
        else if (Math.Abs(Input.GetAxisRaw(Global.VerticalAxis)) < Global.Tolerance)
            NoVerticalActions();
    }

    private void NoVerticalActions()
    {
        verticalAxis.Value = 0;
        if (verticalSinglePress.Value)
            _isVerticalAxisInUse = false;
        nonVerticalAxisEvent.Raise();
    }

    private void UpDirectionActions()
    {
        verticalAxis.Value = 1;
        if (verticalSinglePress.Value)
            _isVerticalAxisInUse = true;
        upButtonEvent.Raise();
    }

    private void DownDirectionActions()
    {
        verticalAxis.Value = -1;
        if (verticalSinglePress.Value)
            _isVerticalAxisInUse = true;
        downButtonEvent.Raise();
    }

    #endregion

    private void CheckingStartButton()
    {
        if (Input.GetAxisRaw(Global.StartAxis) != 0 && !isStartAxisInUse)
        {
            startButtonEvent.Raise();
            isStartAxisInUse = true;
        }
        else if (Input.GetAxisRaw(Global.StartAxis) == 0)
            isStartAxisInUse = false;
    }

    private void CheckingSquareButton()
    {
        if (Input.GetAxisRaw(Global.FireAxis) != 0 && !isSquareAxisInUse)
        {
            squareButtonEvent.Raise();
            isSquareAxisInUse = true;
        }
        else if (Input.GetAxisRaw(Global.FireAxis) == 0)
            isSquareAxisInUse = false;
    }

    private void CheckingXButton()
    {
        if (Input.GetAxisRaw(Global.JumpAxis) != 0 && !isXAxisInUse)
        {
            xButtonEvent.Raise();
            isXAxisInUse = true;
        }
        else if (Input.GetAxisRaw(Global.JumpAxis) == 0)
            isXAxisInUse = false;
    }

    private void CheckChangeButtonUi()
    {
        if (uiPanelActive.Value)
            uiChangeEvent.Raise();
    }
}