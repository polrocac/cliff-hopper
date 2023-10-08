using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Moneda : MonoBehaviour
{

    public float velocidadAngular = 360f;

    public UnityEvent OnPickupCoin;

    // https://www.youtube.com/watch?v=djW7g6Bnyrc&ab_channel=Contraband

    // Start is called before the first frame update
    void Start()
    {
        OnPickupCoin.AddListener(GameManager.Instance.IncreaseCoins);
        OnPickupCoin.AddListener(UIManager.Instance.UpdateCoinText);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, velocidadAngular * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.MonedasCogidas++;
            OnPickupCoin?.Invoke();
            OnPickupCoin.RemoveAllListeners();
            Destroy(gameObject);
            SoundManager.Instance.SelectAudio(4, 0.5f);
            //GameManager.Instance.UpdateCoinUI();
        }
    }

}
