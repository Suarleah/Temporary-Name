using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    private Image fadePanel;
    private Animator animator;
    private int sceneNumber;
    void Start()
    {
        fadePanel = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
        Debug.Log(animator);

        animator.SetTrigger("fadeIn");
    }


    public void FadeOut(string sceneName)
    {
        animator.SetTrigger("fadeOut"); // This animations calls LoadScene at frame 60
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName != null) 
        {
            SceneManager.LoadScene(sceneName);
        }
    }



    

}
