using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallCondition : MonoBehaviour, DefaultInputAction.IUITouchActions
{
    DefaultInputAction _inputAction;    // Input System

    [HideInInspector]
    public bool isBlue = true;      // ���� ���� �Ķ������� �ƴ����� ��Ÿ���� Bool ����

    Material mat;                   // ���� Material

    private void OnEnable() {
        if (_inputAction == null)
            _inputAction = new DefaultInputAction();
        
        _inputAction.UITouch.SetCallbacks(this);
        _inputAction.Enable();
    }

    private void OnDisable() {
        if (_inputAction != null)
            _inputAction.Disable();
    }

    void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// ���� ���� ��ȯ�մϴ�.
    /// </summary>
    public void ChangeColor() {
        isBlue = !isBlue;
        if (isBlue) {
            mat.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }
        else {
            mat.color = new Color(1.0f, 0.5f, 0.0f, 1.0f);
        }
    }

    // �ڽ��� ������ ���� ȭ�� ��ġ �� �� �ٲ�
    void DefaultInputAction.IUITouchActions.OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed && BoxSlide.isStart) {
            ChangeColor();
        }
    }

    void DefaultInputAction.IUITouchActions.OnSlide(InputAction.CallbackContext context)
    {
        
    }
}
