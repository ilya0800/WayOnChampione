using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartedTimeline : MonoBehaviour
{
    private PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        ActiveTimeLine();
    }

    private void ActiveTimeLine()
    {
        if (EnemyHpBarAndStamina.instance.PlayTimeLine)
        {
            playableDirector.Play();
            Destroy(gameObject, 5);
        }
    }
}
