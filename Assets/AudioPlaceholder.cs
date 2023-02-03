using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioPlaceholder : MonoBehaviour
{
    public Animator fading;
    public static AudioPlaceholder instance;
    public AudioSource grab;
    public AudioSource clicked;
    public AudioSource hovered;
    public AudioSource letgo;
    private void Awake()
    {
        fading = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabCoin()
    {
        if(!grab.isPlaying)
        {
            grab.Play();
        }
        
    }

    public void Clicked()
    {
        if(!clicked.isPlaying)
        {
            clicked.Play();
        }
    }

    public void Hovered()
    {
        if(!hovered.isPlaying)
        {
            hovered.Play();
        }
    }

    public void LetGo()
    {
        if(!letgo.isPlaying)
        {
            letgo.Play();
        }
    }

    public void NextScene()
    {
        StartCoroutine(GoNextScene());
    }

    IEnumerator GoNextScene()
    {
        fading.SetTrigger("Fade");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StopCoroutine(GoNextScene());
    }
}
