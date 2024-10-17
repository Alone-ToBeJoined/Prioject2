using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private bool isRunning;

    [SerializeField] private Animator anim;
    [SerializeField] private DynamicJoystick dynamicJoystick;      
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;

    [SerializeField] private ColorType playerColor;

    [SerializeField] private float maxSlopeAngle;

    private float _horizontal;

    private float _vertical;

    private string currentAnimName;

    private RaycastHit _slopeHit;

    public ColorType PlayerColor { get => playerColor; set => playerColor = value; }

    private void Awake()
    {
       
    }

    private void Update()
    {
        if (GetInput())
        {
            ChangeAnim(Constant.RunAnimName);
            if (OnSlope())
            {
                SlopeMove();
            }

            else
            {
                Move();
            }
            LookAtMoveDirection();
        }

        else
        {
            ChangeAnim(Constant.IdleAnimName);
            rb.velocity = Vector3.zero;
        }

        //dung nhan vat khi het gach
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
    }

    #region ChangeAnim

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(animName);
        }
    }

    #endregion

    #region Movement

    public bool GetInput()
    {
        _vertical = dynamicJoystick.Vertical;
        _horizontal = dynamicJoystick.Horizontal;
        if (Mathf.Abs(_vertical) < 0.01f &&
            Mathf.Abs(_horizontal) < 0.01f)
        {
            return false;
        }
        return true;
    }

    public void Move()
    {
        if (rb != null);
        rb.velocity = new Vector3(_horizontal, 0, _vertical).normalized * moveSpeed;
    }

    public void LookAtMoveDirection()
    {
        transform.eulerAngles = new Vector3(0f, 90f + Mathf.Atan2(-_vertical, _horizontal) * 180 / Mathf.PI, 0f);
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, 0.8f))
        {
            Debug.DrawRay(transform.position, Vector3.down * 0.8f, Color.red, 3f);
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public void SlopeMove()
    {
        Vector3 moveDirection = new Vector3(_horizontal, _vertical, 0).normalized;
        rb.velocity = Vector3.ProjectOnPlane(moveDirection, _slopeHit.normal).normalized * moveSpeed;
    }

    #endregion

    private void StopMove()
    {

    }
}

