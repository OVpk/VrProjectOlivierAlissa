using System.Linq;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public void SmallInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand))
            {
                ActionManager.changeCard?.Invoke(EnumHand.LeftHand);
                return;
            }
            ActionManager.changeCard?.Invoke(EnumHand.RightHand);
        }
    }

    public void BigInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand))
            {
                ActionManager.spawnCard?.Invoke(EnumHand.LeftHand);
                return;
            }
            ActionManager.spawnCard?.Invoke(EnumHand.RightHand);
        }

        else if (ctx.canceled)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand))
            {
                ActionManager.removeCard?.Invoke(EnumHand.LeftHand);
                return;
            }
            ActionManager.removeCard?.Invoke(EnumHand.RightHand);
        }
    }

    public void UpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            Debug.Log("pressed UP");
    }

    public void DownInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            Debug.Log("pressed DOWN");
    }
}
