// Copyright (c) 2020 by Meinrad Recheis (Member of SciSharp)
// Code generated by CodeMinion: https://github.com/SciSharp/CodeMinion

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Python.Runtime;
using Numpy.Models;
#if PYTHON_INCLUDED
using Python.Included;
#endif

namespace Numpy
{
    public static partial class np
    {
        
        public static PyObject self => _lazy_self.Value;
        
        private static Lazy<PyObject> _lazy_self = new Lazy<PyObject>(() => 
        {
            try
            {
                return InstallAndImport();
            }
            catch (Exception)
            {
                // retry to fix the installation by forcing a repair, if Python.Included is used.
                return InstallAndImport(force: true);
            }
        }
        );
        
        private static PyObject InstallAndImport(bool force = false)
        {
            #if PYTHON_INCLUDED
            Installer.SetupPython(force).Wait();
            #endif
            #if PYTHON_INCLUDED
            Installer.InstallWheel(typeof(np).Assembly, "numpy-1.21.3-cp310-cp310-win_amd64.whl").Wait();
            #endif
            PythonEngine.Initialize();
            var mod = Py.Import("numpy");
            return mod;
        }
        
        public static dynamic dynamic_self => self;
        private static bool IsInitialized => self != null;
        
        
        public static void Dispose()
        {
            self?.Dispose();
        }
        
        
        //auto-generated
        private static PyTuple ToTuple(Array input)
        {
            var array = new PyObject[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                array[i]=ToPython(input.GetValue(i));
            }
            return new PyTuple(array);
        }
        
        //auto-generated
        internal static PyObject ToPython(object obj)
        {
            if (obj == null) return Runtime.None;
            switch (obj)
            {
                // basic types
                case int o: return new PyInt(o);
                case long o: return new PyInt(o);
                case float o: return new PyFloat(o);
                case double o: return new PyFloat(o);
                case string o: return new PyString(o);
                case bool o: return ConverterExtension.ToPython(o);
                case PyObject o: return o;
                // sequence types
                case Array o: return ToTuple(o);
                // special types from 'ToPythonConversions'
                case Axis o: return o.Axes==null ? null : ToTuple(o.Axes);
                case Shape o: return ToTuple(o.Dimensions);
                case Slice o: return o.ToPython();
                case PythonObject o: return o.PyObject;
                case Dictionary<string, NDarray> o: return ToDict(o);
                default: throw new NotImplementedException($"Type is not yet supported: { obj.GetType().Name}. Add it to 'ToPythonConversions'");
            }
        }
        
        //auto-generated
        internal static T ToCsharp<T>(dynamic pyobj)
        {
            switch (typeof(T).Name)
            {
                // types from 'ToCsharpConversions'
                case "Dtype": return (T)(object)new Dtype(pyobj);
                case "NDarray": return (T)(object)new NDarray(pyobj);
                case "NDarray`1":
                switch (typeof(T).GenericTypeArguments[0].Name)
                {
                   case "Byte": return (T)(object)new NDarray<byte>(pyobj);
                   case "Short": return (T)(object)new NDarray<short>(pyobj);
                   case "Boolean": return (T)(object)new NDarray<bool>(pyobj);
                   case "Int32": return (T)(object)new NDarray<int>(pyobj);
                   case "Int64": return (T)(object)new NDarray<long>(pyobj); 
                   case "Single": return (T)(object)new NDarray<float>(pyobj); 
                   case "Double": return (T)(object)new NDarray<double>(pyobj); 
                   default: throw new NotImplementedException($"Type NDarray<{typeof(T).GenericTypeArguments[0].Name}> missing. Add it to 'ToCsharpConversions'");
                }
                break;
                case "NDarray[]":
                   var po = pyobj as PyObject;
                   var len = po.Length();
                   var rv = new NDarray[len];
                   for (int i = 0; i < len; i++)
                       rv[i] = ToCsharp<NDarray>(po[i]);
                   return (T) (object) rv;
                case "Matrix": return (T)(object)new Matrix(pyobj);
                default:
                var pyClass = $"{pyobj.__class__}";
                if (pyClass == "<class 'str'>")
                {
                    return (T)(object)pyobj.ToString();
                }
                if (pyClass.StartsWith("<class 'numpy"))
                {
                    return (pyobj.item() as PyObject).As<T>();
                }
                try
                {
                    return pyobj.As<T>();
                }
                catch (Exception e)
                {
                    throw new NotImplementedException($"conversion from {pyobj.__class__} to {typeof(T).Name} not implemented", e);
                    return default(T);
                }
            }
        }
        
        //auto-generated
        internal static T SharpToSharp<T>(object obj)
        {
            if (obj == null) return default(T);
            switch (obj)
            {
                // from 'SharpToSharpConversions':
                case Array a:
                if (typeof(T)==typeof(NDarray)) return (T)(object)ConvertArrayToNDarray(a);
                break;
            }
            throw new NotImplementedException($"Type is not yet supported: { obj.GetType().Name}. Add it to 'SharpToSharpConversions'");
        }
        
        //auto-generated: SpecialConversions
        private static NDarray ConvertArrayToNDarray(Array a)
        {
            switch(a)
            {
                case bool[] arr: return np.array(arr);
                case int[] arr: return np.array(arr);
                case float[] arr: return np.array(arr);
                case double[] arr: return np.array(arr);
                case int[,] arr: return np.array(arr.Cast<int>().ToArray()).reshape(arr.GetLength(0), arr.GetLength(1));
                case float[,] arr: return np.array(arr.Cast<float>().ToArray()).reshape(arr.GetLength(0), arr.GetLength(1));
                case double[,] arr: return np.array(arr.Cast<double>().ToArray()).reshape(arr.GetLength(0), arr.GetLength(1));
                case bool[,] arr: return np.array(arr.Cast<bool>().ToArray()).reshape(arr.GetLength(0), arr.GetLength(1));
                default: throw new NotImplementedException($"Type {a.GetType()} not supported yet in ConvertArrayToNDarray.");
            }
        }
        
        //auto-generated: SpecialConversions
        private static PyDict ToDict(Dictionary<string, NDarray> d)
        {
            var dict = new PyDict();
            foreach (var pair in d)
                dict[new PyString(pair.Key)] = pair.Value.self;
            return dict;
        }
    }
}
