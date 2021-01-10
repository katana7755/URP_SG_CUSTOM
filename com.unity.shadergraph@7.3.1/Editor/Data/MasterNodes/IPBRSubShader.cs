using System;

namespace UnityEditor.ShaderGraph
{
    interface IPBRSubShader : ISubShader
    {
        void GetPartialCodesFromSlots(IMasterNode masterNode, out string[] slotNames, out string[] codes);
    }
}
