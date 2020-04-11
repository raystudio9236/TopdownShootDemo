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
        var playerEntity = Contexts.sharedInstance.game.CreateEntity();
        playerEntity.isPlayerTag = true;
        playerEntity.AddPosComp(Vector2.zero);
        playerEntity.AddVelComp(new Vector2(1, 0));

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
