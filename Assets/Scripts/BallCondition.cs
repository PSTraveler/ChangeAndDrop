using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallCondition : MonoBehaviour, DefaultInputAction.IUITouchActions
{
    DefaultInputAction _inputAction;    // Input System

    [HideInInspector]
    public bool isBlue = true;      // 공의 색이 파란색인지 아닌지를 나타내는 Bool 변수

    Material mat;                   // 공의 Material

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
    /// 공의 색을 전환합니다.
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

    // 박스를 뒤집은 이후 화면 터치 시 색 바꿈
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
