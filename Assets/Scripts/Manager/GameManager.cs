using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameSystems _gameSystems;

    private void Awake()
    {
        _gameSystems = new GameSystems(Contexts.sharedInstance);
    }

    private void Start()
    {
        _gameSystems.Initialize();
    }

    private void Update()
    {
        _gameSystems.Execute();
        _gameSystems.Cleanup();
    }

    private void OnDestroy()
    {
        _gameSystems.TearDown();
    }
}