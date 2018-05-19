using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms2D;
using System.Collections.Generic;

public static class Prefabs
{
    public static Prefab Asteroid = new Prefab()
        .AddPhysics()        
        .Add(new Asteroid{})
        .Add(new SceneLayer{})
        .Add(new CircleSprite{});        

    public const long SHIP_RADIUS = 4000; 
    public static Prefab Ship = new Prefab()
        .AddPhysics(radius:SHIP_RADIUS)
        .Add(new Thruster{})  
        .Add(new Health{})
        .Add(new Gun{})
        .Add(new CircleSprite { _Radius = SHIP_RADIUS, _Color = Color.green });        

    public static Prefab AIShip = new Prefab(Ship)
        .Add(new AI{});

    public static Prefab PlayerShip = new Prefab(Ship)
        .Add(new PlayerInput{});

    public const long BULLET_RADIUS = 400; 
    public static Prefab Bullet = new Prefab()
        .AddPhysics(radius:BULLET_RADIUS)
        .Add(new SceneLayer{})
        .Add(new Bullet{})
        .Add(new CircleSprite { _Radius = BULLET_RADIUS, _Color = Color.yellow });        

    public static Prefab AddPhysics(this Prefab prefab, long radius = 1000)
    {
        return prefab
                .Add(new Position{})
                .Add(new Rotation{})
                .Add(new RigidBody{})
                .Add(new CircleCollider{_Radius = radius}); 
    }   
}