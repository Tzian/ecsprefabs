using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms2D;

public struct Id
{
	int _Hash;

    public Id(string str)
    {
    	_Hash = str.GetHashCode();
    }

    public static implicit operator Id(string str)
    {
        return new Id(str);
    }

    public override string ToString()
    {
    	return _Hash.ToString();
    }
}