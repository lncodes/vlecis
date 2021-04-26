using System;

namespace Lncodes.Pacakge.Vlecis
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class NameAttribute : Attribute 
    {
        public readonly string Name;

        public NameAttribute(string name) => Name = name;
    }
}