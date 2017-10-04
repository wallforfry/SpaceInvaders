using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace SpaceInvaders
{
    public class Entity : IEquatable<Entity>
    {
        private Dictionary<Type, IComponent> components;


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
        }

    }
}