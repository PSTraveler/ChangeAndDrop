using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxSlide : MonoBehaviour, DefaultInputAction.IUITouchActions
{
    public static bool isStart = false;     // 박스가 뒤집히고 게임이 시작되었는지 나타내는 Bool 변수

    bool isHold = false;                    // 터치 후 홀드 중인지 나타내는 Bool 변수
    Vector2 pivot;                          // 처음 터치한 포인트 위치
    Vector3 origin;                         // 박스의 처음 위치
    float posX;                             // 홀드 후 슬라이드한 거리
    DefaultInputAction _inputAction;        // Input System
    Animator anim;                          // 박스가 뒤집히는 애니메이션

    private void OnEnable() {
        if (_inputAction == null)
            _inputAction = new DefaultInputAction();

        _inputAction.UITouch.SetCallbacks(this);
        _inputAction.Enable();
    }

    private void OnDisable() {
        if (_inputAction != null) {
            _inputAction.Disable();
        }
    }

    // X축 슬라이드의 위치 변화값을 받아옵니다.
    void DefaultInputAction.IUITouchActions.OnSlide(InputAction.CallbackContext context)
    {
        if (context.performed) {
            posX = context.ReadValue<float>() * 0.01f;
        }
    }

    // 터치를 인식합니다. 손을 땠을 경우 게임이 시작됩니다.
    void DefaultInputAction.IUITouchActions.OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isHold = true;
        }

        if (context.canceled) {
            isHold = false;
            anim.SetTrigger("Start");
            isStart = true;
            enabled = false;
        }
    }

    private void Start() {
        origin = transform.position;

        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 홀드 시에 슬라이드대로 박스를 움직입니다.
        if (isHold)
        {
            transform.position += Vector3.forward * posX;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -1.0f, 1.0f));
        }
    }
}
