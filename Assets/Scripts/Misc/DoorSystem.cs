using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class DoorSystem : MonoBehaviour
{

    public UnityEvent OnDoorOpened = new UnityEvent();
    public UnityEvent OnDoorClosed = new UnityEvent();

    public bool startOpen = false;

    public PlayableAsset openAnimation;
    public PlayableAsset closeAnimation;

    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        if (director == null)
        {
            Debug.LogErrorFormat("PlayableDirector component missing on {0}", name);
            return;
        }
        director.playOnAwake = false;
        director.playableAsset = null;
    }

    private void Start()
    {
        if (startOpen)
        {
            ForceOpenDoors();
        }
        else
        {
            ForceCloseDoors();
        }
    }

    public void OpenDoor()
    {
        if (director.playableAsset == openAnimation)
        {
            return;
        }

        float progress = (float)(director.time / director.duration);
        director.playableAsset = openAnimation;
        director.time = director.duration * (1.0f - progress);
        if (director.time != openAnimation.duration)
        {
            director.stopped -= DoorsClosed;
            director.stopped += DoorsOpened;
            director.Play();
        }
    }

    public void CloseDoor()
    {
        if (director.playableAsset == closeAnimation)
        {
            return;
        }

        float progress = (float)(director.time / director.duration);
        director.playableAsset = closeAnimation;
        director.time = director.duration * (1.0f - progress);
        if (director.time != closeAnimation.duration)
        {
            director.stopped -= DoorsOpened;
            director.stopped += DoorsClosed;
            director.Play();
        }
    }

    public void ForceOpenDoors()
    {
        director.playableAsset = openAnimation;
        director.Play();
        director.time = openAnimation.duration;
    }

    public void ForceCloseDoors()
    {
        director.playableAsset = closeAnimation;
        director.Play();
        director.time = closeAnimation.duration;
    }

    private void DoorsOpened(PlayableDirector playableDirector)
    {
        playableDirector.stopped -= DoorsOpened;
        OnDoorOpened.Invoke();
    }

    private void DoorsClosed(PlayableDirector playableDirector)
    {
        playableDirector.stopped -= DoorsClosed;
        OnDoorClosed.Invoke();
    }

}
