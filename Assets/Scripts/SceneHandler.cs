using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public RectTransform fader;

    private void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha (fader, 1, 0);
        LeanTween.alpha (fader, 0, 0.5f).setOnComplete (() => {
            fader.gameObject.SetActive (false);
        });
    }

    public void TransitionToPlay()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            Invoke("Play", 0f);
        });
    }

    public void TransitionToHTP()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            Invoke("HTP", 0f);
            fader.gameObject.SetActive(false);

        });
    }

    public void TransitionBackToMenu()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            Invoke("Menu", 0f);
            fader.gameObject.SetActive(false);
        });
    }


    public void TransitionToCredits()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            Invoke("Credits", 0f);
            fader.gameObject.SetActive(false);

        });
    }

    public void TransitionToMenuFromGame()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 0, 0);
        LeanTween.alpha(fader, 1, 0.5f).setOnComplete(() =>
        {
            Invoke("MenuFromGame", 0f);
        });
    }


    public void Exit()
    {
        Application.Quit();
    }

    private void Credits()
    {
        this.transform.parent.Find("CREDITS").gameObject.SetActive(true);
        this.transform.parent.Find("MainMenu").gameObject.SetActive(false);
    }

    private void Menu()
    {
        this.transform.parent.Find("MainMenu").gameObject.SetActive(true);
        this.transform.parent.Find("HTP").gameObject.SetActive(false);
        this.transform.parent.Find("CREDITS").gameObject.SetActive(false);
    }


    private void HTP()
    {
        this.transform.parent.Find("HTP").gameObject.SetActive(true);
        this.transform.parent.Find("MainMenu").gameObject.SetActive(false);
        //transform.Find("MainMenu").gameObject.SetActive(false);
        //transform.Find("CREDITS").gameObject.SetActive(false);
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void MenuFromGame(){
        SceneManager.LoadScene(0);
    }

}
