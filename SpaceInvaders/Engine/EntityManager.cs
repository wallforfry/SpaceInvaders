using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Windows.Forms.VisualStyles;

namespace SpaceInvaders
{
    public class EntityManager
    {
        Entity this[int index] => _entities[index];
 
        // Entités, indexés par leur ID unique
        private readonly Dictionary<int, Entity> _entities;
 
        // "Matrice" de composants, l'essentiel des données de jeu est stocké ici.
        // La première clé est le type de composant.
        // La seconde clé est l'ID de l'entité.
        private readonly Dictionary<Type, Dictionary<int, IComponent>> _components;
 
        // Gestion de la durée de vie d'une entité
        Entity CreateEntity()
        {
            Entity entity = new Entity();
            int uid = DateTime.Now.Second+DateTime.Now.Millisecond.GetHashCode();
            _entities.Add(uid,entity);
            return entity;
        }

        bool DestroyEntity(int id)
        {
            foreach (var keys in _components.Keys)
            {
                IComponent component;
                if (_components[keys].TryGetValue(id, out component))
                {
                    _components[keys].Remove(id);
                }
                
            }
            return true;
        }
 
        // Gestion de la composition d'une entité
        TComponent CreateComponent<TComponent>(int entityId)
            where TComponent : IComponent, new()
        {          
            TComponent component = new TComponent();
            _components[typeof(TComponent)][entityId] = component;

            return component;
        }

        bool RemoveComponent<TComponent>(int entityId)
            where TComponent : IComponent, new()
        {
            _components[typeof(TComponent)].Remove(entityId);
            return true;
        }

        TComponent GetComponent<TComponent>(int entityId)
            where TComponent : class, IComponent, new()
        {
            return _components[typeof(TComponent)][entityId] as TComponent;
        }

        IEnumerable<IComponent> GetComponents(int entityId)
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
        CompositionNode<TComposition> GetNodes()
            where TComposition : CompositionBase {...}
    }
}