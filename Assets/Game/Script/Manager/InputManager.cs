using System.Linq;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;

    public void SmallInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if(GameManager.CurrentGameState == GameState.InRound)
            {
                ActionManager.changeCard?.Invoke(EnumHand.RightHand);
            }
        }
        else if (ctx.canceled)
        {
        }
    }

    public void BigInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand) && GameManager.CurrentGameState == GameState.InRound)
            {
                player.TryShoot();
                return;
            }
            else if(GameManager.CurrentGameState == GameState.InRound && ctx.control.device.usages.Contains(CommonUsages.RightHand))
            {
                ActionManager.spawnCard?.Invoke(EnumHand.RightHand);
            }
                
        }

        else if (ctx.canceled)
        {
             if(ctx.control.device.usages.Contains(CommonUsages.RightHand) && GameManager.CurrentGameState == GameState.InRound)
                ActionManager.removeCard?.Invoke(EnumHand.RightHand);
        }
    }

}
