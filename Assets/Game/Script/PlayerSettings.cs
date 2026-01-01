using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private Slider heightSlider;
    [SerializeField] private Transform camera;
    [SerializeField] private XROrigin xrOrigin;


    private float baseHeight;
    private Vector3 basePosition;

    private void Start()
    {
        basePosition =transform.position;
        baseHeight = basePosition.y;
    }
    public void HeightChange()
    {
        transform.position = new Vector3(transform.position.x, baseHeight * heightSlider.value, transform.position.z);
    }

    public void ResetPosition()
    {

        Vector3 cameraLocalPos = xrOrigin.Camera.transform.localPosition;

        xrOrigin.transform.position = new Vector3(basePosition.x, transform.position.y, basePosition.z) - new Vector3(
            cameraLocalPos.x,
            0f,
            cameraLocalPos.z
        );
    }
}
