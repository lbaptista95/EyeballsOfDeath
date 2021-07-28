using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Animator standardCharAnimator;

    private PlayableDirector director;
    private RuntimeAnimatorController controller;

    private void OnEnable()
    {
        director = GetComponent<PlayableDirector>();
        controller = standardCharAnimator.runtimeAnimatorController;
        standardCharAnimator.runtimeAnimatorController = null;

        director.enabled = true;
        StartCoroutine(WaitForCutsceneEnd());
    }

    //Wait for the cutscene to end, so the core loop can start
    private IEnumerator WaitForCutsceneEnd()
    {
        yield return new WaitUntil(() => director.state != PlayState.Playing);

        director.enabled = false;

        standardCharAnimator.runtimeAnimatorController = controller;

        ProjectilesManager.instance.StartSpawning();
    }
}
