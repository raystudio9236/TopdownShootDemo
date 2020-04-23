using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Contexts Contexts => Instance._contexts;

    private Contexts _contexts;
    private GameSystems _gameSystems;
    private FixedUpdateGameSystems _fixedUpdateGameSystems;

    public static GameEntity GetEntity(int id)
    {
        return Contexts.game.GetEntityWithIdComp(id);
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;

        _contexts = Contexts.sharedInstance;
        _contexts.SubscribeId();

        var physicsEntity = _contexts.physics.CreateEntity();
        physicsEntity.AddPhysicsComp(
            new List<CollisionInfo>());

        _gameSystems = new GameSystems(_contexts);
        _fixedUpdateGameSystems = new FixedUpdateGameSystems(_contexts);
    }

    private void Start()
    {
        _gameSystems.Initialize();
    }

    private void FixedUpdate()
    {
        _fixedUpdateGameSystems.Execute();
        _fixedUpdateGameSystems.Cleanup();
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