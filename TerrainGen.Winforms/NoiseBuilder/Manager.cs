using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NoiseMixer;
using BindingFlags = System.Reflection.BindingFlags;

namespace TerrainGen.Winforms.NoiseBuilder
{
    public static class NoiseManager
    {
        public static IEnumerable<Type> GetNoiseTypes()
        {
            var type = typeof(INoise);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in asm.GetTypes())
                {
                    if (t.GetInterfaces().Contains(type) && !t.IsInterface)
                    {
                        yield return t;
                    }
                }
            }
        }

        public static List<ParameterInfo> GetNoiseConstructorParams(Type noise)
        {
            var ctors = noise.GetConstructors();
            foreach (var ctor in ctors)
            {
                var parameters = ctor.GetParameters();
                if (parameters.Length == 0)
                {
                    return new List<ParameterInfo>();
                }
                else 
                {
                    return parameters.ToList();
                }
            }

            return new List<ParameterInfo>();
        }
    }
}
