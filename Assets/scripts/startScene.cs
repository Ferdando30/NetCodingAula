using UnityEngine;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
   
    public void StartGame (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
