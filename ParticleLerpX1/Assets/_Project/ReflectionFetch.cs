using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ParticleInformation
{
    public float maxSpeed { get; set; }
    public float maxSize { get; set; }
}



public class ReflectionFetch : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        var particleInfo = new ParticleInformation
        {
            maxSize = 100,
            maxSpeed = 120
        };
        DumpObject(particleInfo);
    }

    void DumpObject(object particleInfo)
    {
        Debug.Log($"Hash: {particleInfo.GetHashCode()}");
        Debug.Log($"Type: {particleInfo.GetType()}");

        FieldInfo [] fieldInfo = particleInfo.GetType().GetFields();

        TypedReference tr = TypedReference.MakeTypedReference(particleInfo,fieldInfo);

        

        var props = GetProperties(particleInfo);

        foreach (var prop in props)
        {
            Debug.Log($"Key: {prop.Key}  and value: {prop.Value}");            
        }
    }

    private Dictionary<string, string> GetProperties(object obj)
    {
        var props = new Dictionary<string,string>();
        var type = obj.GetType();
        foreach (var prop in type.GetProperties())
        {
            var val = prop.GetValue(obj);
            props.Add(prop.Name,val.ToString());
        }
        return props;
    }
}
