using System;

namespace WellsFargo.Libraries.CRMSystem.ORM.Interfaces
{
    public interface ISystemContext : IDisposable
    {
        bool CommitChanges();
    }
}
