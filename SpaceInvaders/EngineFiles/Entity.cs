using System.Collections.Generic;
using SpaceInvaders.Components;

namespace SpaceInvaders.EngineFiles
{
    public class Entity
    {
        // La manager responsable de cette entité
        private readonly EntityManager _manager;

        public Entity(int id)
        {
            Id = id;
            _manager = new EntityManager();
        }


        public int Id { get; }

        public List<IComponent> GetComponents => _manager.GetComponents(Id);

        public TComponent GetComponent<TComponent>()
            where TComponent : class, IComponent, new()
        {
            return _manager.GetComponent<TComponent>(Id);
        }

        public bool HasComponent<TComponent>()
            where TComponent : class, IComponent, new()
        {
            return _manager.HasComponent<TComponent>(Id);
        }

        public TComponent CreateComponent<TComponent>()
            where TComponent : IComponent, new()
        {
            // C'est le manager qui fait tout le travail
            var component = _manager.CreateComponent<TComponent>(Id);
            return component;
        }

        public void DestroyComponent<TComponent>()
            where TComponent : IComponent, new()
        {
            _manager.RemoveComponent<TComponent>(Id);
        }
    }
}