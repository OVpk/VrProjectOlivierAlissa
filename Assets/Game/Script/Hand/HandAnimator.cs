using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private InputActionReference triggerActionReference;
    [SerializeField] private InputActionReference gripActionReference;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();   
        SetUpInputActions();
    }

    private void SetUpInputActions()
    {
        if(triggerActionReference != null && gripActionReference != null)
        {
            triggerActionReference.action.performed += ctx => UpdateHandAnimation("Trigger", ctx.ReadValue<float>());
            triggerActionReference.action.canceled += ctx => UpdateHandAnimation("Trigger", 0);

            gripActionReference.action.performed += ctx => UpdateHandAnimation("Grip", ctx.ReadValue<float>());
            gripActionReference.action.canceled += ctx => UpdateHandAnimation("Grip", 0);
        }
    }

    private void UpdateHandAnimation(string pName, float pValue)
    {
        if(animator != null)
        {
            animator.SetFloat(pName, pValue);
        }
    }

    private void OnEnable()
    {
        triggerActionReference?.action.Enable();
        gripActionReference?.action.Enable();
    }

    private void OnDisable()
    {
        triggerActionReference?.action.Disable();
        gripActionReference?.action.Disable();
    }
}
