using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject boulaPrefab;

    public float velHorizontal;

    private Player player;
    private Guide guide;

    public GameObject fog;

    public int Coins { get; private set; } = 0;

    public int Corners { get; private set; } = 0;

    public static GameManager Instance { get; private set; }
    public int MonedasCogidas { get; set; }

    bool victory = false;

    public int highscore = 0;

    //public int Bioma { get; set; }    

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            Invoke("SpawnBola", 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnBola()
    {
        
        GameObject boula = Instantiate(boulaPrefab, LevelGenerator.Instance.spawnPoint + Vector3.up, Quaternion.identity);
        boula.GetComponent<Boula>().SetSpeed(velHorizontal*0.975f);
    }

    public void setPlayerAndGuide(Player p, Guide g)
    {
        player = p;
        guide = g;
    }

    public void ChangeFogColor(Color c, float transitionTime)
    {
        fog.GetComponent<Fog>().ChangeFogColor(c, transitionTime);
    }

    public void IncreaseCoins(){
        ++Coins;
    }


    public void IncreaseCorners()
    {
        ++Corners;
    }

    public void Victory()
    {
        if(!victory) {
            SoundManager.Instance.SelectAudio(8, 0.5f);
            UIManager.Instance.ActivarMenuMuerte();
        }
        victory = true;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        SoundManager.Instance.Pause();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        SoundManager.Instance.Resume();
    }

    public void setHighscore(int hs){
        if(hs > HighScore.Instance.Highscore)
        {
            HighScore.Instance.Highscore = hs;
        }
        UIManager.Instance.UpdateHighScore();

    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
}
