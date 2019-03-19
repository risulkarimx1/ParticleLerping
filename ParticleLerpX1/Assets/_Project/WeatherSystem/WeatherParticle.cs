using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
/// <summary>
/// custom editor class
/// <see cref="WeatherParticleEditor"/>
/// </summary>
namespace AAI.VDTSimulator.Weather
{
    public static class ReflectionExtensions
    {
        public static bool IsCustomValueType(this Type type)
        {
            return type.IsValueType && !type.IsPrimitive && type.Namespace != null &&
                   !type.Namespace.StartsWith("System.");
        }
    }
    [CreateAssetMenu(fileName = "Particle Setup Name", menuName = "AAI/Weather/Particle Setup")]

    public class WeatherParticle : ScriptableObject
    {
        [SerializeField] private ParticleSystem _baseParticleSystem;
        [SerializeField] private ParticleSystem _baseParticleSystem_Max;

        #region Min Max Start Size
        [SerializeField] private Vector2 _minMaxStartSize_Min;
        [SerializeField] private Vector2 _minMaxStartSize_Max;

        #endregion

        

        [SerializeField] private float _emissionRateOverTime;

        public void PopulateData()
        {
            ParticleSystem.MainModule main = _baseParticleSystem.main;
            ParticleSystem.EmissionModule emissionModule = _baseParticleSystem.emission;
            _minMaxStartSize_Min = new Vector2(main.startSize.constantMin, main.startSize.constantMax);
            _emissionRateOverTime = emissionModule.rateOverTime.constant;


            Type particleType = typeof(ParticleSystem);
            PropertyInfo[] propertyInfos = particleType.GetProperties();
            foreach (PropertyInfo property in propertyInfos)
            {
                Debug.Log($"_________________________________________________________________________");
                Debug.Log($"property: {property.Name}, | Type {property.PropertyType}");
                Debug.Log($"Base Particle Value: {property.GetValue(_baseParticleSystem)}");
                Debug.Log($"Max Particle Value: {property.GetValue(_baseParticleSystem_Max)}");

                bool isEqual = property.GetValue(_baseParticleSystem)
                    .Equals(property.GetValue(_baseParticleSystem_Max));
                if(isEqual)
                    Debug.Log($"Are they equal: {isEqual}");
                else
                    Debug.LogWarning($"Are they equal: {isEqual}");
                if (isEqual)
                {
                    continue;
                }

                bool isCustom = property.PropertyType.IsCustomValueType();
                Debug.Log($"Is it a Custom value ? {isCustom}");
                if (!isCustom)
                {
                    continue;
                }

                //Debug.Log($"    ######## Going inside for Property {property.Name} ##########");
                //PropertyInfo[] properties = property.PropertyType.GetProperties();
                //foreach (PropertyInfo prop in properties)
                //{
                //    Debug.Log($"        ******* Components of {property.Name} ******");
                //    Debug.Log($"	    ->Field Name: {prop.Name}");
                //    Debug.Log($"	    ->Field Type: {prop.PropertyType}");
                //    Debug.Log($"	    ->Value: {prop.GetValue(prop)}");
                    
                //    //Debug.Log($"	->Value Type For Base: {porp.GetValue(_baseParticleSystem)}");
                //    //Debug.Log($"	->Value Type For Max: {porp.GetValue(_baseParticleSystem_Max)}");
                //    //Debug.Log($"	->Are they equal: {porp.GetValue(_baseParticleSystem).Equals(porp.GetValue(_baseParticleSystem_Max))}");
                //}
                //Debug.Log($"    ######## END for Property {property.Name} ##########");



            }

            //var fieldValues = _baseParticleSystem.GetType()
            //	.GetFields(bindingFlags)
            //	.Select(field => field.Name)
            //	.ToList();

            //Debug.Log($"size is : {fieldValues.Count}");
            //foreach (var filed in fieldValues)
            //{
            //	Debug.Log($"Field Name : {nameof(filed)} ");
            //}

            //Debug.Log($"in the list");
            //foreach (FieldInfo field in _baseParticleSystem.GetType().GetFields(bindingFlags))
            //{
            //	Debug.Log($"field.Name {field.Name}");
            //}

        }
    }
}


