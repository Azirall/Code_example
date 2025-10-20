using System;

namespace _Project.Scripts.Mining_System
{
    public interface IResourceNode
    {
        bool CanUse();
        void BeginUse();
        void CancelUse();
        void SetHighlight(bool enabled);
        event Action<ItemData> Tick;
        event Action Cancelled;
    }
}
