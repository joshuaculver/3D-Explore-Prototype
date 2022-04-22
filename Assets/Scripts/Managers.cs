using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(NameofManager))]
[RequireComponent(typeof(DialogueManager))]
[RequireComponent(typeof(PlayerManager))]

public class Managers : MonoBehaviour
{
    public static DialogueManager Dialogue {get; private set;}
    public static PlayerManager Player {get; private set;}

    private List<IGameManager> _startSequence;

    void Awake()
    {
        Dialogue = GetComponent<DialogueManager>();
        Player = GetComponent<PlayerManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Dialogue);
        _startSequence.Add(Player);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);

            yield return null;
        }

        Debug.Log("All managers started");
    }
}
