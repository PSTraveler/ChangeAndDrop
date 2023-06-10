using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxSlide : MonoBehaviour, DefaultInputAction.IUITouchActions
{
    public static bool isStart = false;     // �ڽ��� �������� ������ ���۵Ǿ����� ��Ÿ���� Bool ����

    bool isHold = false;                    // ��ġ �� Ȧ�� ������ ��Ÿ���� Bool ����
    Vector2 pivot;                          // ó�� ��ġ�� ����Ʈ ��ġ
    Vector3 origin;                         // �ڽ��� ó�� ��ġ
    float posX;                             // Ȧ�� �� �����̵��� �Ÿ�
    DefaultInputAction _inputAction;        // Input System
    Animator anim;                          // �ڽ��� �������� �ִϸ��̼�

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

    // X�� �����̵��� ��ġ ��ȭ���� �޾ƿɴϴ�.
    void DefaultInputAction.IUITouchActions.OnSlide(InputAction.CallbackContext context)
    {
        if (context.performed) {
            posX = context.ReadValue<float>() * 0.01f;
        }
    }

    // ��ġ�� �ν��մϴ�. ���� ���� ��� ������ ���۵˴ϴ�.
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
        // Ȧ�� �ÿ� �����̵��� �ڽ��� �����Դϴ�.
        if (isHold)
        {
            transform.position += Vector3.forward * posX;
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -1.0f, 1.0f));
        }
    }
}
