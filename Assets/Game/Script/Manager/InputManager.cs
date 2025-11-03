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
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand) && GameManager.instance.CurrentGameState == GameState.InRound)
            {
                ActionManager.GunAppear?.Invoke(EnumHand.LeftHand);
                return;
            }
            else if(GameManager.instance.CurrentGameState == GameState.InRound)
                ActionManager.changeCard?.Invoke(EnumHand.RightHand);
        }
        else if (ctx.canceled)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand) && GameManager.instance.CurrentGameState == GameState.InRound)
            {
                ActionManager.GunDisapear?.Invoke(EnumHand.LeftHand);
                return;
            }
        }
    }

    public void BigInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand) && GameManager.instance.CurrentGameState == GameState.InRound)
            {
                ActionManager.playerShoot?.Invoke();
                return;
            }
            else if(GameManager.instance.CurrentGameState == GameState.InRound && ctx.control.device.usages.Contains(CommonUsages.RightHand))
            {
                ActionManager.spawnCard?.Invoke(EnumHand.RightHand);
            }
                
        }

        else if (ctx.canceled)
        {
             if(ctx.control.device.usages.Contains(CommonUsages.RightHand) && GameManager.instance.CurrentGameState == GameState.InRound)
                ActionManager.removeCard?.Invoke(EnumHand.RightHand);
        }
    }

}
