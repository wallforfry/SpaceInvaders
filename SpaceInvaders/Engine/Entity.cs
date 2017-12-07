using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace SpaceInvaders
{
    public class Entity
    {
        public Entity(int id)
        {
            this.Id = id;
            this._manager = new EntityManager();
        }
        
        
        public int Id { get; }
        
        public List<IComponent> GetComponents {
            get
            {
                return _manager.GetComponents(this.Id);
            }
        }
        
        public TComponent GetComponent<TComponent>()
                where TComponent : class, IComponent, new()
        {
            return _manager.GetComponent<TComponent>(this.Id);
        }

        public bool HasComponent<TComponent>()
            where TComponent : class, IComponent, new()
        {
            return _manager.HasComponent<TComponent>(this.Id);
        }
        
        // La manager responsable de cette entité
        private readonly EntityManager _manager;
 
        public TComponent CreateComponent<TComponent>()
            where TComponent : IComponent, new()
        {
            // C'est le manager qui fait tout le travail
            var component = _manager.CreateComponent<TComponent>(this.Id);
            return component;
        }
 
        public void DestroyComponent<TComponent>()
        where TComponent : IComponent, new()
        {
            _manager.RemoveComponent<TComponent>(this.Id);

        }
             

    }
}