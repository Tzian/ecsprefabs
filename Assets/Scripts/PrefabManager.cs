using Unity.Entities;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Assertions;

public class Prefab
{
	interface ComponentInfo
	{
		System.Type GetComponentType();
		void Set(Entity entity, EntityManager entity_manager);
		bool HasDefaultValues();
	} 

	class ComponentInfo<ComponentType> : ComponentInfo where ComponentType : struct, IComponentData
	{
		public ComponentType _Component;

		public ComponentInfo(ComponentType component)
		{
			_Component = component;
		}

		public void Set(Entity entity, EntityManager entity_manager)
		{
			entity_manager.SetComponentData(entity, _Component);
		}		

		public System.Type GetComponentType()
		{
			return typeof(ComponentType);
		}

		public bool HasDefaultValues()
		{
			return _Component.Equals(System.Activator.CreateInstance<ComponentType>());
		}
	}

	List<ComponentInfo> _Components = new List<ComponentInfo>();
	List<ComponentInfo> _NonDefaultComponents = new List<ComponentInfo>();
	EntityArchetype _Archetype;
	EntityManager _EntityManager;

	public Prefab()
	{

	}

	public Prefab(Prefab parent)
	{
		_Components.AddRange(parent._Components);
	}

	public Prefab Add<ComponentType>(ComponentType component) where ComponentType : struct, IComponentData
	{
        Assert.IsTrue(_EntityManager == null);

        _Components.Add(new ComponentInfo<ComponentType>(component));
		return this;
	}

	public Entity Spawn()
	{		
		Assert.IsTrue(_EntityManager != null);

		Entity entity = _EntityManager.CreateEntity(_Archetype);

		foreach(ComponentInfo component in _NonDefaultComponents)
		{
			component.Set(entity, _EntityManager);
		}

		return entity;
	}

	public void Prepare(EntityManager entity_manager)
	{
        Assert.IsTrue(_EntityManager == null);

        _EntityManager = entity_manager;

		List<ComponentType> types = new List<ComponentType>();
		foreach(ComponentInfo cmp in _Components)
		{
			types.Add(cmp.GetComponentType());
			if(!cmp.HasDefaultValues())
			{
				_NonDefaultComponents.Add(cmp);
			}
		}

		_Archetype = entity_manager.CreateArchetype(types.ToArray());
	}
}

public class PrefabManager
{
	Dictionary<Id, Prefab> _Prefabs = new Dictionary<Id, Prefab>();

	public void RegisterPrefab(Id id, Prefab prefab)
	{
		Assert.IsTrue(!_Prefabs.ContainsKey(id));

		_Prefabs[id] = prefab;
	}

	public Entity Spawn(Id id)
	{
		Assert.IsTrue(_Prefabs.ContainsKey(id));

		return _Prefabs[id].Spawn();
	}

	public void CollectPrefabs()
	{
        foreach (System.Type type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes())
        {
        	foreach(FieldInfo field in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
        	{
        		if(field.FieldType != typeof(Prefab)) continue;

        		RegisterPrefab(field.Name, (Prefab)field.GetValue(null));
        	}
        }
	}

	public void PreparePrefabs()
	{
		EntityManager entity_manager = World.Active.GetOrCreateManager<EntityManager>();
		foreach(KeyValuePair<Id, Prefab> kvp in _Prefabs)
		{
			kvp.Value.Prepare(entity_manager);
		}
	}
}