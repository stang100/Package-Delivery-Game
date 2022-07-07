using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    //private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // void Awake()
    // {
    //     canvasGroup = GetComponent<CanvasGroup>();
    // }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyUp (KeyCode.M)) {
        //     if (canvasGroup.interactable) {
        //         canvasGroup.interactable = false;
        //         canvasGroup.blocksRaycasts = false;
        //         canvasGroup.alpha = 0f;
        //         Time.timeScale = 1.0f;
        //     } else {
        //         canvasGroup.interactable = true;
        //         canvasGroup.blocksRaycasts = true;
        //         canvasGroup.alpha = 1f;
        //         Time.timeScale = 0.0f;
        //     } 
        // }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
            QuitGame();
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
