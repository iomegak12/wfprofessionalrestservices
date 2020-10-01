using System;
using System.Collections.Generic;

namespace WellsFargo.Libraries.DataAccess.Interfaces
{
    public interface IRepository<EntityType, EntityKey> : IDisposable
    {
        IEnumerable<EntityType> GetAllEntities();
        EntityType GetEntityByKey(EntityKey entityKey);
        bool AddNewEntity(EntityType entityType);
        EntityType UpdateEntity(EntityType existingEntityType);
        bool DeleteEntity(EntityKey entityKey);
    }
}
