using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms2D;

public static class InitializePrefabManager
{
    static PrefabManager _PrefabManager;

    public static void Initialize()
    {
        _PrefabManager = new PrefabManager();
        _PrefabManager.CollectPrefabs();
        _PrefabManager.PreparePrefabs();        
    }
}

public class Example : ComponentSystem
{
    struct Data
    {
        public int Length;
        public ComponentDataArray<Gun> _Guns;
        public ComponentDataArray<Position> _Positions;        
    }

    [Inject] Data _Data;

    protected override void OnCreateManager(int capacity)
    {
        InitializePrefabManager.Initialize();

        Prefabs.PlayerShip.Spawn()
            .Set(new Position{_X = 100, _Y = 100});        
    }

    protected override void OnUpdate()
    {
        for(int i = 0; i < _Data.Length; ++i)
        {
            PostUpdateCommands.Spawn(Prefabs.Bullet)
                .Set(_Data._Positions[i]);
        }    
    }
}