using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Transforms2D;

public static class EntityUtil
{
	public static Entity Add<ComponentType>(this Entity entity, ComponentType component) where ComponentType : struct, IComponentData
	{
		EntityManager entity_manager = World.Active.Get<EntityManager>();
		entity_manager.AddComponent(entity, typeof(ComponentType));
		entity_manager.SetComponentData(entity, component);
		return entity;
	}

	public static Entity Set<ComponentType>(this Entity entity, ComponentType component) where ComponentType : struct, IComponentData
	{
		EntityManager entity_manager = World.Active.Get<EntityManager>();
		entity_manager.SetComponentData(entity, component);
		return entity;		
	}

	public static ComponentType Get<ComponentType>(this Entity entity) where ComponentType : struct, IComponentData
	{
		EntityManager entity_manager = World.Active.Get<EntityManager>();
		return entity_manager.GetComponentData<ComponentType>(entity);
	}	

	public static bool Has<ComponentType>(this Entity entity) where ComponentType : struct, IComponentData
	{
		EntityManager entity_manager = World.Active.Get<EntityManager>();
		return entity_manager.HasComponent<ComponentType>(entity);
	}	


	public static void Destroy(this Entity entity)
	{
		EntityManager entity_manager = World.Active.Get<EntityManager>();
		entity_manager.DestroyEntity(entity);		
	}

	public static ManagerType Get<ManagerType>(this World world) where ManagerType : ScriptBehaviourManager
	{
		return world.GetOrCreateManager<ManagerType>();
	}

	public static EntityCommandBuffer Set<ComponentType>(this EntityCommandBuffer command_buffer, ComponentType component) where ComponentType : struct, IComponentData
	{
		command_buffer.SetComponent(component);
		return command_buffer;
	}
}