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
                ActionManager.playerShoot?.Invoke();
                return;
            }
            else if(GameManager.instance.CurrentGameState == GameState.InRound)
                ActionManager.changeCard?.Invoke(EnumHand.RightHand);
        }
    }

    public void BigInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand) && GameManager.instance.CurrentGameState == GameState.InRound)
            {
                ActionManager.GunAppear?.Invoke(EnumHand.LeftHand);
                return;
            }
            else if(GameManager.instance.CurrentGameState == GameState.InRound)
            {
                ActionManager.spawnCard?.Invoke(EnumHand.RightHand);
                Debug.Log("input");
            }
                
        }

        else if (ctx.canceled)
        {
            if (ctx.control.device.usages.Contains(CommonUsages.LeftHand) && GameManager.instance.CurrentGameState == GameState.InRound)
            {
                ActionManager.GunDisapear?.Invoke(EnumHand.LeftHand);
                return;
            }
            else if(GameManager.instance.CurrentGameState == GameState.InRound)
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
