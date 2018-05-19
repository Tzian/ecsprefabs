using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms2D;

public struct AI : IComponentData
{
    
}

public struct Asteroid : IComponentData
{
    
}

public struct Bullet : IComponentData
{
    public Entity _Gun;
}

public struct CircleSprite : IComponentData
{
    public long _Radius;
    public Color _Color;
}

public struct CircleCollider : IComponentData
{
    public long _Radius;
}

public struct long2
{
    public long _X;
    public long _Y;

    public long2(long x, long y)
    {
        _X = x;
        _Y = y;
    }
}

public struct Gun : IComponentData
{

}

public struct Health : IComponentData
{
    public long _Value;
}

public struct PlayerInput : IComponentData
{
    
}

public struct Position : IComponentData
{
    public long _X;
    public long _Y;

    public Position(long x, long y)
    {
        _X = x;
        _Y = y;
    }

    public float2 ToFloat2()
    {
        return new float2((float)_X, (float)_Y);
    }

    public static implicit operator long2(Position pos)
    {
        return new long2(pos._X, pos._Y);
    }

    public static implicit operator Position(long2 pos)
    {
        return new Position(pos._X, pos._Y);
    }    
}

public struct RigidBody : IComponentData
{
	public long _Mass;
	public long2 _Velocity;
    public long2 _Force;
}

public struct Rotation : IComponentData
{
    public long _Angle;
}

public struct SceneLayer : IComponentData
{
    public long _Layer;
}

public struct SpawnPoint : IComponentData
{
    public long _SpawnPoint;
}

public struct Thruster : IComponentData
{
    public long _IsThrusting;
    public long _IsBoosting;
}

public struct TrackPosition : IComponentData
{
    public Entity _TargetEntity;
}