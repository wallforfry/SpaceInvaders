﻿using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
 
  public class CompositionNodes<TComposition>
    where TComposition : CompositionBase, new()
  {
    // Toutes les entités qui matchent la composition sont exposées via ces "nodes".
    public List<TComposition> Nodes => _nodes.Values.ToList();

    // En interne, les nodes sont indexées par l'ID de l'entité propriétaire pour être facilement récupérée
    private readonly Dictionary<int, TComposition> _nodes = new Dictionary<int, TComposition>();

    //On regarde à quoi l'entité est élligible
    public void Inspect(Entity entity)
    {
      
      var matching = Matches(entity);

      // L'entité match TComposition
      if (matching && !_nodes.ContainsKey(entity.Id))
      {
        
        var node = CreateNode(entity);

        _nodes.Add(entity.Id, node);
      }
      // L'entité matchait mais plus maintenant
      else if (!matching && _nodes.ContainsKey(entity.Id))
      {
        var node = _nodes[entity.Id];
        _nodes.Remove(entity.Id);
      }
    }

    //Vérifie qu'une entité match avec la composition demandée
    private bool Matches(Entity entity)
    {
      if (typeof(TComposition) == typeof(RenderComposition))
      {
        return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
               entity.HasComponent<LifeComponent>();
      }

      if (typeof(TComposition) == typeof(MovableComposition))
      {
        return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
               entity.HasComponent<PhysicsComponent>() && entity.HasComponent<LifeComponent>();
      }

      if (typeof(TComposition) == typeof(PlayerComposition))
      {
        return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
               entity.HasComponent<PhysicsComponent>() && entity.HasComponent<LifeComponent>() && 
               entity.HasComponent<FireComponent>() && entity.HasComponent<TypeComponent>() && entity.GetComponent<TypeComponent>().TypeOfObject == TypeOfObject.CONTROLABLE;
      }

      if (typeof(TComposition) == typeof(AIComposition))
      {
        return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
               entity.HasComponent<PhysicsComponent>() && entity.HasComponent<LifeComponent>() &&
               entity.HasComponent<FireComponent>() && entity.HasComponent<EnemyBlockComponent>()
               && entity.HasComponent<TypeComponent>();
      }

      
      if (typeof(TComposition) == typeof(CollisionComposition))
      {
        return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
               entity.HasComponent<LifeComponent>() && entity.HasComponent<TypeComponent>();
      }
      
      return false;
    }

    //Crée le node demandé
    private TComposition CreateNode(Entity entity)         
    {
      if (typeof(TComposition) == typeof(RenderComposition))       
      {
        TComposition composition = Activator.CreateInstance<RenderComposition>() as TComposition;


          composition.Owner = entity;
          (composition as RenderComposition).Position = entity.GetComponent<PositionComponent>();
          (composition as RenderComposition).Render = entity.GetComponent<RenderComponent>();
          (composition as RenderComposition).Life = entity.GetComponent<LifeComponent>();         
        
        return composition;
      }
      
      
      if (typeof(TComposition) == typeof(MovableComposition))       
      {
        TComposition composition = Activator.CreateInstance<MovableComposition>() as TComposition;

          composition.Owner = entity;
          (composition as MovableComposition).Position = entity.GetComponent<PositionComponent>();
          (composition as MovableComposition).Render = entity.GetComponent<RenderComponent>();
          (composition as MovableComposition).Physic = entity.GetComponent<PhysicsComponent>();         
          (composition as MovableComposition).Life = entity.GetComponent<LifeComponent>();         
        
        return composition;
      }
      
      if (typeof(TComposition) == typeof(PlayerComposition))       
      {
        TComposition composition = Activator.CreateInstance<PlayerComposition>() as TComposition;

          composition.Owner = entity;
          (composition as PlayerComposition).Position = entity.GetComponent<PositionComponent>();
          (composition as PlayerComposition).Render = entity.GetComponent<RenderComponent>();
          (composition as PlayerComposition).Physic = entity.GetComponent<PhysicsComponent>(); 
          (composition as PlayerComposition).Life = entity.GetComponent<LifeComponent>(); 
          (composition as PlayerComposition).Fire = entity.GetComponent<FireComponent>();                
          (composition as PlayerComposition).TypeComponent = entity.GetComponent<TypeComponent>();    
        
        return composition;
      }
      
      if (typeof(TComposition) == typeof(AIComposition))       
      {
        TComposition composition = Activator.CreateInstance<AIComposition>() as TComposition;

        composition.Owner = entity;
        (composition as AIComposition).Position = entity.GetComponent<PositionComponent>();
        (composition as AIComposition).Render = entity.GetComponent<RenderComponent>();
        (composition as AIComposition).Physic = entity.GetComponent<PhysicsComponent>(); 
        (composition as AIComposition).Life = entity.GetComponent<LifeComponent>(); 
        (composition as AIComposition).Fire = entity.GetComponent<FireComponent>();        
        (composition as AIComposition).Enemy = entity.GetComponent<EnemyBlockComponent>();        
        (composition as AIComposition).TypeComponent= entity.GetComponent<TypeComponent>();    
        
        return composition;
      }
      
      if (typeof(TComposition) == typeof(CollisionComposition))       
      {
        TComposition composition = Activator.CreateInstance<CollisionComposition>() as TComposition;

        composition.Owner = entity;
        (composition as CollisionComposition).Position = entity.GetComponent<PositionComponent>();
        (composition as CollisionComposition).Render = entity.GetComponent<RenderComponent>(); 
        (composition as CollisionComposition).Life = entity.GetComponent<LifeComponent>();  
        (composition as CollisionComposition).TypeComponent= entity.GetComponent<TypeComponent>();    
        
        return composition;
      }
      
      return null;
    }
  }
}