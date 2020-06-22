using System;
using UnityEngine;

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

    [SerializeField] private GameEvent anyDirectionalAxisEvent;
    //[SerializeField] private GameEvent noDirectionalAxisEvent;

    #endregion

    #region Camera Buttons
    [SerializeField] private FloatReference mouseVerticalAxis;
    [SerializeField] private FloatReference mouseHorizontalAxis;
    [SerializeField] private GameEvent cameraLeftEvent;
    [SerializeField] private GameEvent cameraRightEvent;
    [SerializeField] private GameEvent cameraUpEvent;
    [SerializeField] private GameEvent cameraDownEvent;
    [SerializeField] private GameEvent anyCameraAxisEvent;
    [SerializeField] private GameEvent noMouseVerticalAxis;
    [SerializeField] private GameEvent noMouseHorizontalAxis;
    #endregion

    #region Action Buttons
    [Header("Action Buttons Variables")]
    [SerializeField] private GameEvent startButtonEvent;
    [SerializeField] private GameEvent quitButtonEvent;
    [SerializeField] private GameEvent fireButtonEvent;
    [SerializeField] private GameEvent confirmButtonEvent;

    private bool _isStartAxisInUse = false;
    private bool _isQuitAxisInUse = false;
    private bool _isFireAxisInUse = false;
    private bool _isConfirmAxisInUse = false;
    #endregion
    
    [Header("UI Active Variables")]
    [SerializeField] private BoolReference uiPanelActive;
    [SerializeField] private GameEvent uiChangeEvent;

    private void Update()
    {
        CheckingVerticalAxis();
        CheckingHorizontalAxis();
        CheckingMouseVerticalAxis();
        CheckingMouseHorizontalAxis();
        CheckingStartButton();
        CheckingQuitButton();
        CheckingFireButton();
        CheckingConfirmButton();
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
        anyDirectionalAxisEvent.Raise();
    }

    private void LeftDirectionActions()
    {
        horizontalAxis.Value = -1;
        if (horizontalSinglePress.Value)
            _isHorizontalAxisInUse = true;
        leftButtonEvent.Raise();
        anyDirectionalAxisEvent.Raise();
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
        anyDirectionalAxisEvent.Raise();
    }

    private void DownDirectionActions()
    {
        verticalAxis.Value = -1;
        if (verticalSinglePress.Value)
            _isVerticalAxisInUse = true;
        downButtonEvent.Raise();
        anyDirectionalAxisEvent.Raise();
    }

    #endregion

    #region Camera Vertical Functions

    private void CheckingMouseVerticalAxis()
    {
        var mouseVerticalValue = Input.GetAxisRaw(Global.MouseVerticalAxis);
        if (mouseVerticalValue < 0)
            DownRotationActions(mouseVerticalValue);
        else if (mouseVerticalValue > 0)
            UpRotationActions(mouseVerticalValue);
        else if (Math.Abs(mouseVerticalValue) < Global.Tolerance)
            NoMouseVerticalActions();
    }

    private void NoMouseVerticalActions()
    {
        mouseVerticalAxis.Value = 0;
        noMouseVerticalAxis.Raise();
    }

    private void UpRotationActions(float mouseVerticalValue)
    {
        mouseVerticalAxis.Value = mouseVerticalValue;
        cameraUpEvent.Raise();
        anyCameraAxisEvent.Raise();
    }

    private void DownRotationActions(float mouseVerticalValue)
    {
        mouseVerticalAxis.Value = mouseVerticalValue;
        cameraDownEvent.Raise();
        anyCameraAxisEvent.Raise();
    }

    #endregion

    #region Camera Horizontal Functions

    private void CheckingMouseHorizontalAxis()
    {
        var mouseHorizontalValue = Input.GetAxisRaw(Global.MouseHorizontalAxis);
        if (mouseHorizontalValue < 0)
            LeftRotationActions(mouseHorizontalValue);
        else if (mouseHorizontalValue > 0)
            RightRotationActions(mouseHorizontalValue);
        else if (Math.Abs(mouseHorizontalValue) < Global.Tolerance)
            NoMouseHorizontalActions();
    }

    private void NoMouseHorizontalActions()
    {
        mouseHorizontalAxis.Value = 0;
        noMouseHorizontalAxis.Raise();
    }

    private void RightRotationActions(float mouseHorizontalValue)
    {
        mouseHorizontalAxis.Value = mouseHorizontalValue;
        cameraRightEvent.Raise();
        anyCameraAxisEvent.Raise();
    }

    private void LeftRotationActions(float mouseHorizontalValue)
    {
        mouseHorizontalAxis.Value = mouseHorizontalValue;
        cameraLeftEvent.Raise();
        anyCameraAxisEvent.Raise();
    }

    #endregion

    #region Action Functions

    private void CheckingStartButton()
    {
        if (Input.GetAxisRaw(Global.StartAxis) != 0 && !_isStartAxisInUse)
        {
            startButtonEvent.Raise();
            _isStartAxisInUse = true;
        }
        else if (Math.Abs(Input.GetAxisRaw(Global.StartAxis)) < Global.Tolerance)
            _isStartAxisInUse = false;
    }

    private void CheckingQuitButton()
    {
        if (Input.GetAxisRaw(Global.QuitAxis) != 0 && !_isQuitAxisInUse)
        {
            quitButtonEvent.Raise();
            _isQuitAxisInUse = true;
        }
        else if (Math.Abs(Input.GetAxisRaw(Global.QuitAxis)) < Global.Tolerance)
            _isQuitAxisInUse = false;
    }

    private void CheckingFireButton()
    {
        if (Input.GetAxisRaw(Global.FireAxis) != 0 && !_isFireAxisInUse)
        {
            fireButtonEvent.Raise();
            _isFireAxisInUse = true;
        }
        else if (Math.Abs(Input.GetAxisRaw(Global.FireAxis)) < Global.Tolerance)
            _isFireAxisInUse = false;
    }

    private void CheckingConfirmButton()
    {
        if (Input.GetAxisRaw(Global.JumpAxis) != 0 && !_isConfirmAxisInUse)
        {
            confirmButtonEvent.Raise();
            _isConfirmAxisInUse = true;
        }
        else if (Math.Abs(Input.GetAxisRaw(Global.JumpAxis)) < Global.Tolerance)
            _isConfirmAxisInUse = false;
    }

    #endregion

    #region UI Functions

    private void CheckChangeButtonUi()
    {
        if (uiPanelActive.Value)
            uiChangeEvent.Raise();
    }

    #endregion
}