using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBGMChange : MonoBehaviour
{
    public int index = 0;
    public void Start()
    {
        GameAudioManager.instance.PlayBossMusic(index);
    }
}
