using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    private Image fadePanel;
    private Animator animator;
    private string sceneName;
    void Start()
    {
        fadePanel = GetComponentInChildren<Image>();
        animator = GetComponent<Animator>();
        Debug.Log(animator);

        animator.SetTrigger("fadeIn");
    }


    public void FadeOut(string s)
    {
        if (s != null)
        {
            sceneName = s;
            animator.SetTrigger("fadeOut"); // This animations calls LoadScene at frame 60
        }

    }

    public void LoadScene()
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }





}
