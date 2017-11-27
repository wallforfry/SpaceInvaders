using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SpaceInvaders
{
    public class EntityManager
    {
        public EntityManager()
        {
            this._components = new Dictionary<Type, Dictionary<int, IComponent>>();
            this._entities = new Dictionary<int, Entity>();
        }
        
        Entity this[int index] => _entities[index];
 
        // Entités, indexés par leur ID unique
        private Dictionary<int, Entity> _entities;
 
        // "Matrice" de composants, l'essentiel des données de jeu est stocké ici.
        // La première clé est le type de composant.
        // La seconde clé est l'ID de l'entité.
        private readonly Dictionary<Type, Dictionary<int, IComponent>> _components;
 
        // Gestion de la durée de vie d'une entité
        public Entity CreateEntity()
        {
            Random rdm = new Random();
            int uid = DateTime.Now.Second+DateTime.Now.Millisecond.GetHashCode()+rdm.Next();
            Entity entity;
            if (_entities.ContainsKey(uid))
            {
                entity = new Entity(uid + rdm.Next());

            }
            else
            {
                entity = new Entity(uid);
            }
            _entities.Add(uid, entity);
            return entity;
        }

        public bool DestroyEntity(int id)
        {
            foreach (var value in _components)
            {               
                IComponent component;
                if (_components[value.Key].TryGetValue(id, out component))
                {
                    _components[value.Key].Remove(id);
                }

            }

            _entities.Remove(id);
            return true;
        }
 
        // Gestion de la composition d'une entité
        public TComponent CreateComponent<TComponent>(int entityId)
            where TComponent : IComponent, new()
        {
            TComponent component = new TComponent();
            Dictionary<int, IComponent> dictionary = new Dictionary<int, IComponent>();            
            dictionary.Add(entityId, component);
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
            TComponent component;
            Dictionary<int, IComponent> dictionary;
            try
            {
                return _components[typeof(TComponent)][entityId] as TComponent;
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                return null;
            }
            
            return null;
        }
        
        public bool HasComponent<TComponent>(int entityId)
            where TComponent : class, IComponent, new()
        {
            TComponent component;
            Dictionary<int, IComponent> dictionary;
            try
            {
                component = _components[typeof(TComponent)][entityId] as TComponent;
                if (component != null)
                    return true;
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                return false;
            }
            
            return false;
        }

        public List<IComponent> GetComponents(int entityId)
        {
            List<IComponent> result = new List<IComponent>();

            foreach (var keys in _components.Keys)
            {
                IComponent component;
                if (_components[keys].TryGetValue(entityId, out component))
                {
                    result.Add(component);   
                }
                
            }
            return result;
        }
 
        // Retourne des "vues" sur les entités dont la composition
        // correspond à TComposition (plus de détails sur cette partie plus tard)
        public CompositionNodes<TComposition> GetNodes<TComposition> ()
            where TComposition : CompositionBase, new()
        {
            CompositionNodes<TComposition> compositionNodes = new CompositionNodes<TComposition>();
            foreach (var entity in _entities)
            {
                compositionNodes.Inspect(entity.Value);
            }
            return compositionNodes;
        }
    }
}