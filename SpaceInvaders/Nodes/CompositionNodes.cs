using System;
using System.Collections.Generic;
using System.Linq;
using SpaceInvaders.Nodes;

namespace SpaceInvaders
{
  
  // Une collection contenant une node pour chaque entité
  // qui match TComposition. L'EntityManager maintient
  // autant de ces collections qu'il y a de types de compositions.
  public class CompositionNodes<TComposition>
    where TComposition : CompositionBase, new()
  {
    // Toutes les entités qui matchent la composition sont exposées via ces "nodes".
    public List<TComposition> Nodes => _nodes.Values.ToList();

    // En interne, les nodes sont indexées par l'ID de l'entité propriétaire.
    private readonly Dictionary<int, TComposition> _nodes = new Dictionary<int, TComposition>();

    // Les systèmes logiques se branchent sur ces events pour
    // être notifiés de nouvelles entités matchant TComposition 
    // bien d'entités ne le matchant plus.
    public event Action<TComposition> OnNodeAdded;

    public event Action<TComposition> OnNodeRemoved;

    // Cette méthode est appelée par l'EntityManager lorsque
    // la composition d'une entité change afin de vérifier si
    // elle match désormais TComposition.
    // Si c'est le cas, une nouvelle node avec des références
    // aux composants en question sera créée pour l'entité.
    public void Inspect(Entity entity)
    {
      // En pratique, on utilise des masques de bits
      // pour rapidement matcher les entités.
      var matching = Matches(entity);

      // L'entité match TComposition
      if (matching && !_nodes.ContainsKey(entity.Id))
      {

        // Derrière cet appel, on utilise de la réfléxion pour
        // instancier le node et peupler ses champs avec des
        // références directes vers les composants de l'entité.
        var node = CreateNode(entity);

        _nodes.Add(entity.Id, node);
        OnNodeAdded?.Invoke(node);
      }
      // L'entité matchait TComposition, mais ce n'est plus le cas
      else if (!matching && _nodes.ContainsKey(entity.Id))
      {
        var node = _nodes[entity.Id];
        OnNodeRemoved?.Invoke(node);
        _nodes.Remove(entity.Id);
      }
    }

    private bool Matches(Entity entity)
    {
      return true;
    }

    private TComposition CreateNode(Entity entity)         
    {
      if (typeof(TComposition) == typeof(RenderComposition))       
      {
        TComposition composition = Activator.CreateInstance<RenderComposition>() as TComposition;

        if (true)
        {
          composition.Owner = entity;
          (composition as RenderComposition).Position = entity.GetComponent<PositionComponent>();
          (composition as RenderComposition).Render = entity.GetComponent<RenderComponent>();
          (composition as RenderComposition).Life = entity.GetComponent<LifeComponent>(); 
        }
        
        return composition;
      }
      
      
      if (typeof(TComposition) == typeof(MovableComposition))       
      {
        TComposition composition = Activator.CreateInstance<MovableComposition>() as TComposition;

        if (true)
        {
          composition.Owner = entity;
          (composition as MovableComposition).Position = entity.GetComponent<PositionComponent>();
          (composition as MovableComposition).Render = entity.GetComponent<RenderComponent>();
          (composition as MovableComposition).Physic = entity.GetComponent<PhysicsComponent>(); 
        }
        
        return composition;
      }
      
      if (typeof(TComposition) == typeof(PlayerComposition))       
      {
        TComposition composition = Activator.CreateInstance<PlayerComposition>() as TComposition;

        if (true)
        {
          composition.Owner = entity;
          (composition as PlayerComposition).Position = entity.GetComponent<PositionComponent>();
          (composition as PlayerComposition).Render = entity.GetComponent<RenderComponent>();
          (composition as PlayerComposition).Physic = entity.GetComponent<PhysicsComponent>(); 
          (composition as PlayerComposition).Life = entity.GetComponent<LifeComponent>(); 
        }
        
        return composition;
      }
      
      return null;
    }
  }
}