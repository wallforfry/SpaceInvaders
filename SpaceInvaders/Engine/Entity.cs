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
            set
            {
                
            }
        }
        
        public TComponent GetComponent<TComponent>()
                where TComponent : class, IComponent, new()
        {
            return _manager.GetComponent<TComponent>(this.Id);
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
        
        /// 
        /// 
        /// 
        /*private Dictionary<Type, IComponent> components;


        public Entity(params IComponent[] componentsToAdd)
        {
            components = new Dictionary<Type, IComponent>();
            foreach (var component in componentsToAdd)
            {
                this.components.Add(component.GetType(), component);
            }

        }

        public Dictionary<Type, IComponent> GetComponents()
        {
            return this.components;
        }

        public bool Equals(Entity other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public IComponent GetComponent(Type type)
        {
            IComponent value = null;
            components.TryGetValue(type, out value);
            return value;
        }*/

    }
}