using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum SceneTransitionType
{
    none,
    middle,
    circle,
    fade,
    sideSwipe,
}

public class TransitionController : MonoBehaviour
{
    public PlayableDirector director;

    [Header("Fader")]
    public GameObject FadeTranstion;
    public PlayableAsset fadeOutAnimation;
    public PlayableAsset fadeInAnimation;


    [SerializeField] private List<GameObject> transitionObject; //all transtion used for hard stop
    public bool sceneClosed; //used to tell if a fade out is already in play. Used for cutscene
    public bool finished = true;

    public void PlayTransition(SceneTransitionType transitionType, bool open)
    {
        finished = false;
        if (open)
        {
            switch (transitionType)
            {
                case SceneTransitionType.fade:
                    director.Play(fadeOutAnimation);
                    sceneClosed = false;
                    break;
            }
        }
        else
        {
            switch (transitionType)
            {
                case SceneTransitionType.fade:
                    director.Play(fadeInAnimation);
                    sceneClosed = true;
                    break;
            }
        }
    }

    public void FinishedTansitionCallBack()
    {
        finished = true;
    }

    public IEnumerator FinishedTransitionCoroutine()
    {
        while (finished == false)
        {
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    public void HardStop()
    {
        director.Stop();
        finished = true;
        for (int i = 0; i < transitionObject.Count; i++)
        {
            transitionObject[i].SetActive(false);
        }
    }
}
