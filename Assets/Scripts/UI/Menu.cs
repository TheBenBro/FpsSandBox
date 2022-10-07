using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GoToScene(string scene_)
    {
        GameManager.Instance.SetLevel(scene_);
        GameManager.Instance.SetGameState(GameManager.GameState.StartGame);
        SceneManager.LoadScene(1);
    }
    public void GoToMainMenu()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Menu);
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
