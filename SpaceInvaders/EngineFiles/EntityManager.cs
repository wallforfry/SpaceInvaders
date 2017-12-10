using System;
using System.Collections.Generic;
using System.Threading;
using SpaceInvaders.Components;
using SpaceInvaders.Nodes;

namespace SpaceInvaders.EngineFiles
{
    public class EntityManager
    {
        // "Matrice" de composants, l'essentiel des données de jeu est stocké ici.
        // La première clé est le type de composant.
        // La seconde clé est l'ID de l'entité.
        private readonly Dictionary<Type, Dictionary<int, IComponent>> _components;

        // Entités, indexés par leur ID unique
        private readonly Dictionary<int, Entity> _entities;

        public EntityManager()
        {
            _components = new Dictionary<Type, Dictionary<int, IComponent>>();
            _entities = new Dictionary<int, Entity>();
        }

        public Dictionary<int, Entity> GetEntities()
        {
            return _entities;
        }

        // Gestion de la durée de vie d'une entité
        public Entity CreateEntity()
        {
            var rdm = new Random();
            Thread.Sleep(10);
            var uid = DateTime.Now.Second + DateTime.Now.Millisecond.GetHashCode() + rdm.Next();
            var entity = _entities.ContainsKey(uid) ? new Entity(uid + rdm.Next()) : new Entity(uid);
            try
            {
                _entities.Add(uid, entity);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Uid de l'entité existe déjà :(");
            }

            return entity;
        }

        public bool DestroyEntity(int id)
        {
            foreach (var value in _components)
            {
                IComponent component;
                if (_components[value.Key].TryGetValue(id, out component))
                    _components[value.Key].Remove(id);
            }

            _entities.Remove(id);
            return true;
        }

        public void ClearGame()
        {
            _entities.Clear();
            _components.Clear();
        }

        // Gestion de la composition d'une entité
        public TComponent CreateComponent<TComponent>(int entityId)
            where TComponent : IComponent, new()
        {
            var component = new TComponent();
            var dictionary = new Dictionary<int, IComponent> {{entityId, component}};
            _components.Add(typeof(TComponent), dictionary);

            return component;
        }

        public bool RemoveComponent<TComponent>(int entityId)
            where TComponent : IComponent
        {
            _components[typeof(TComponent)].Remove(entityId);
            return true;
        }

        public TComponent GetComponent<TComponent>(int entityId)
            where TComponent : class, IComponent, new()
        {           
            try
            {
                return _components[typeof(TComponent)][entityId] as TComponent;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public bool HasComponent<TComponent>(int entityId)
            where TComponent : class, IComponent, new()
        {
            try
            {
                var component = _components[typeof(TComponent)][entityId] as TComponent;
                if (component != null)
                    return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return false;
        }

        public List<IComponent> GetComponents(int entityId)
        {
            var result = new List<IComponent>();

            foreach (var keys in _components.Keys)
            {
                IComponent component;
                if (_components[keys].TryGetValue(entityId, out component))
                    result.Add(component);
            }
            return result;
        }

        // Retourne des "vues" sur les entités dont la composition
        // correspond à TComposition ( les fameux NODES générés à la volée)
        public CompositionNodes<TComposition> GetNodes<TComposition>()
            where TComposition : CompositionBase, new()
        {
            var compositionNodes = new CompositionNodes<TComposition>();
            foreach (var entity in _entities)
                compositionNodes.Inspect(entity.Value);
            return compositionNodes;
        }
    }
}