using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aplastable : MonoBehaviour
{
    [System.NonSerialized]
    public bool aplastado = false;

    public virtual void Aplastar(float scale)
    {
        if (aplastado) return;
        aplastado = true;
        SoundManager.Instance.SelectAudio(7, 0.5f);
        transform.Translate(Vector3.down * (transform.localScale.y - scale) / 2);
        transform.localScale = new Vector3(transform.localScale.x * 2, scale, transform.localScale.z * 2);

    }
}
