using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms2D;

public class Example : ComponentSystem
{
    static PrefabManager _PrefabManager;

    struct Data
    {
        public int Length;
        public ComponentDataArray<Gun> _Guns;
        public ComponentDataArray<Position> _Positions;        
    }

    [Inject] Data _Data;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void AfterSceneLoad()
    {
        _PrefabManager = new PrefabManager();
        _PrefabManager.CollectPrefabs();
        _PrefabManager.PreparePrefabs();

        Prefabs.PlayerShip.Spawn()
            .Set(new Position{_X = 100, _Y = 100});

        int asteroid_count = 400;
        int world_size = 1000000;
        int minimum_asteroid_size = 5;
        int maximum_asteroid_size = 1000;
        for(int i = 0; i < asteroid_count; ++i)
        {
            long x = (long)Random.Range(-world_size, world_size);
            long y = (long)Random.Range(-world_size, world_size);
            long radius = (long)Random.Range(minimum_asteroid_size, maximum_asteroid_size);
            Color color = Color.red;

            Entity asteroid = Prefabs.Asteroid.Spawn();
            
            RigidBody rigid_body = asteroid.Get<RigidBody>();
            asteroid
                .Set(new Position(x, y))
                .Set(rigid_body)
                .Set(new CircleCollider{_Radius = radius})
                .Set(new CircleSprite{_Radius = radius, _Color = color});
        }
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