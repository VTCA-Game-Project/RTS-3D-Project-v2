using UnityEngine;

namespace InterfaceCollection
{
    interface IGameEntiy
    {
        Vector3 Position { get; }
        Vector3 Heading { get; }
        Vector3 Velocity { get; }
    }
}
