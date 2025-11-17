using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    public void OnPlayPressed()
    {
        SceneManager.LoadScene("alissa");
    }

    public void OnTutoPressed()
    {
        SceneManager.LoadScene("tuto");
    }
}
