using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private InputActionReference triggerActionReference;
    [SerializeField] private InputActionReference gripActionReference;

    private Animator animator;

    private const string triggerAnimHand = "Trigger";
    private const string gripAnimHand = "Grip";

    private void Awake()
    {
        animator = GetComponent<Animator>();   
        SetUpInputActions();
    }

    private void SetUpInputActions()
    {
        if(triggerActionReference != null && gripActionReference != null)
        {
            triggerActionReference.action.performed += ctx => UpdateHandAnimation(triggerAnimHand, ctx.ReadValue<float>());
            triggerActionReference.action.canceled += ctx => UpdateHandAnimation(triggerAnimHand, 0);

            gripActionReference.action.performed += ctx => UpdateHandAnimation(gripAnimHand, ctx.ReadValue<float>());
            gripActionReference.action.canceled += ctx => UpdateHandAnimation(gripAnimHand, 0);
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
