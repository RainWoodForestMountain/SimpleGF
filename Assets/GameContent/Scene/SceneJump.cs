using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    void Start ()
    {
        SceneManager.LoadScene("start");
    }
}