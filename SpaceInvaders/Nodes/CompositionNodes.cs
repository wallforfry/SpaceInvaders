using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Components;
using SpaceInvaders.EngineFiles;

namespace SpaceInvaders.Nodes
{
    public class CompositionNodes<TComposition>
        where TComposition : CompositionBase, new()
    {
        // En interne, les nodes sont indexées par l'ID de l'entité propriétaire pour être facilement récupérée
        private readonly Dictionary<int, TComposition> _nodes = new Dictionary<int, TComposition>();

        // Toutes les entités qui matchent la composition sont exposées via ces "nodes".
        public List<TComposition> Nodes => _nodes.Values.ToList();

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
                _nodes.Remove(entity.Id);
            }
        }

        //Vérifie qu'une entité match avec la composition demandée
        private static bool Matches(Entity entity)
        {
            if (typeof(TComposition) == typeof(RenderComposition))
                return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
                       entity.HasComponent<LifeComponent>();

            if (typeof(TComposition) == typeof(MovableComposition))
                return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
                       entity.HasComponent<PhysicsComponent>() && entity.HasComponent<LifeComponent>();

            if (typeof(TComposition) == typeof(PlayerComposition))
                return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
                       entity.HasComponent<PhysicsComponent>() && entity.HasComponent<LifeComponent>() &&
                       entity.HasComponent<FireComponent>() && entity.HasComponent<TypeComponent>() &&
                       entity.GetComponent<TypeComponent>().TypeOfObject == TypeOfObject.Controlable;

            if (typeof(TComposition) == typeof(AiComposition))
                return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
                       entity.HasComponent<PhysicsComponent>() && entity.HasComponent<LifeComponent>() &&
                       entity.HasComponent<FireComponent>() && entity.HasComponent<EnemyBlockComponent>()
                       && entity.HasComponent<TypeComponent>();


            if (typeof(TComposition) == typeof(CollisionComposition))
                return entity.HasComponent<PositionComponent>() && entity.HasComponent<RenderComponent>() &&
                       entity.HasComponent<LifeComponent>() && entity.HasComponent<TypeComponent>();

            return false;
        }

        //Crée le node demandé
        private static TComposition CreateNode(Entity entity)
        {
            if (typeof(TComposition) == typeof(RenderComposition))
            {
                var composition = Activator.CreateInstance<RenderComposition>();

                composition.Owner = entity;
                composition.Position = entity.GetComponent<PositionComponent>();
                composition.Render = entity.GetComponent<RenderComponent>();
                composition.Life = entity.GetComponent<LifeComponent>();

                return composition as TComposition;
            }


            if (typeof(TComposition) == typeof(MovableComposition))
            {
                var composition = Activator.CreateInstance<MovableComposition>();

                composition.Owner = entity;
                composition.Position = entity.GetComponent<PositionComponent>();
                composition.Render = entity.GetComponent<RenderComponent>();
                composition.Physic = entity.GetComponent<PhysicsComponent>();
                composition.Life = entity.GetComponent<LifeComponent>();

                return composition as TComposition;
            }

            if (typeof(TComposition) == typeof(PlayerComposition))
            {
                var composition = Activator.CreateInstance<PlayerComposition>();

                composition.Owner = entity;
                composition.Position = entity.GetComponent<PositionComponent>();
                composition.Render = entity.GetComponent<RenderComponent>();
                composition.Physic = entity.GetComponent<PhysicsComponent>();
                composition.Life = entity.GetComponent<LifeComponent>();
                composition.Fire = entity.GetComponent<FireComponent>();
                composition.TypeComponent = entity.GetComponent<TypeComponent>();

                return composition as TComposition;
            }

            if (typeof(TComposition) == typeof(AiComposition))
            {
                var composition = Activator.CreateInstance<AiComposition>();

                composition.Owner = entity;
                composition.Position = entity.GetComponent<PositionComponent>();
                composition.Render = entity.GetComponent<RenderComponent>();
                composition.Physic = entity.GetComponent<PhysicsComponent>();
                composition.Life = entity.GetComponent<LifeComponent>();
                composition.Fire = entity.GetComponent<FireComponent>();
                composition.Enemy = entity.GetComponent<EnemyBlockComponent>();
                composition.TypeComponent = entity.GetComponent<TypeComponent>();

                return composition as TComposition;
            }

            if (typeof(TComposition) == typeof(CollisionComposition))
            {
                var composition = Activator.CreateInstance<CollisionComposition>();

                composition.Owner = entity;
                composition.Position = entity.GetComponent<PositionComponent>();
                composition.Render = entity.GetComponent<RenderComponent>();
                composition.Life = entity.GetComponent<LifeComponent>();
                composition.TypeComponent = entity.GetComponent<TypeComponent>();

                return composition as TComposition;
            }          

            return null;
        }
    }
}