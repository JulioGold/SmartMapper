using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartMapper
{
    public static class Mapper
    {
        /// <summary>
        /// Carrega as propriedades de um objeto conforme outro objeto passado.
        /// </summary>
        /// <param name="source">Objeto que contém as propriedades.</param>
        /// <param name="target">Objeto que irá receber as propriedades.</param>
        public static void AutoLoad(object source, object target)
        {
            if (source == null || target == null)
            {
                return;
            }

            System.Reflection.PropertyInfo[] propertiesInfo = source.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo sourceProperty in propertiesInfo)
            {
                var targetProperty = target.GetType().GetProperty(sourceProperty.Name);

                if (targetProperty != null)
                {
                    if (sourceProperty.PropertyType == targetProperty.PropertyType)
                    {
                        var sourcePropertyValue = sourceProperty.GetValue(source, null);
                        targetProperty.SetValue(target, sourcePropertyValue, null);
                    }
                }
            }
        }

        /// <summary>
        /// Carrega o valor na propriedade do objeto alvo conforme o que vier do valor do elemento fonte.
        /// </summary>
        /// <param name="target">Objeto alvo.</param>
        /// <param name="targetPropertyName">Nome da propriedade do objeto alvo que receberá o valor.</param>
        /// <param name="elementSource">Elemento fonte.</param>
        /// <param name="element">Elemento de onde será obtido o valor para carregar na propriedade do objeto alvo.</param>
        public static void LoadFromXML(object target, string targetPropertyName, XElement elementSource, Func<XElement, XElement> element)
        {
            var targetProperty = GetPropertyInfo(target, targetPropertyName);
            targetProperty.SetValue(target, Convert.ChangeType(element.Invoke(elementSource).Value, targetProperty.PropertyType), null);
        }

        /// <summary>
        /// Carrega o valor na propriedade do objeto alvo conforme o que vier do valor do elemento fonte.
        /// </summary>
        /// <param name="target">Objeto alvo.</param>
        /// <param name="targetPropertyName">Nome da propriedade do objeto alvo que receberá o valor.</param>
        /// <param name="elementSource">Elemento fonte.</param>
        /// <param name="element">Elemento de onde será obtido o valor para carregar na propriedade do objeto alvo.</param>
        public static void LoadFromXML(object target, string targetPropertyName, XElement elementSource, Func<XElement, XAttribute> element)
        {
            var targetProperty = GetPropertyInfo(target, targetPropertyName);
            targetProperty.SetValue(target, Convert.ChangeType(element.Invoke(elementSource).Value, targetProperty.PropertyType), null);
        }

        private static PropertyInfo GetPropertyInfo(object target, string targetPropertyName) => target.GetType().GetProperty(targetPropertyName);

        // Converter
        //object propvalue = Convert.ChangeType(inputValue, propType);

        // Dinamic casting
        //TypeDescriptor.GetConverter(typeof(String)).ConvertTo(myObject, typeof(Program));

        //var valorResultado = element.Invoke(elementSource).ParseAttributeValue("id", Convert.ToInt32);

        //public static T ParseAttributeValue<T>(this XAttribute element, string attribute, Func<string, T> converter)
        //{
        //    string value = element.Value;

        //    if (String.IsNullOrWhiteSpace(value))
        //    {
        //        return default(T);
        //    }

        //    return converter(value);
        //}

        //public static T ParseAttributeValue<T>(this XElement element, string attribute, Func<string, T> converter)
        //{
        //    string value = element.Attribute(attribute).Value;

        //    if (String.IsNullOrWhiteSpace(value))
        //    {
        //        return default(T);
        //    }

        //    return converter(value);
        //}

#if DEBUG
        // Deixados como private pois ainda é preciso melhorar esses métodos adicionando verificação se o tipo dos objetos é o mesmo...
        private static void AutoLoadLinq(object source, object target)
        {
            if (source == null || target == null)
            {
                return;
            }

            source.GetType().GetProperties()
            .Where(w => target.GetType().GetProperty(w.Name) != null)
            .Select(item =>
            {
                target.GetType().GetProperty(item.Name).SetValue(target, item.GetValue(source, null), null);
                return item;
            });
        }

        // Deixados como private pois ainda é preciso melhorar esses métodos adicionando verificação se o tipo dos objetos é o mesmo...
        private static void AutoLoadParallel(object source, object target)
        {
            if (SmartUtils.SmartUtils.CheckHasNull(source, target))
            {
                return;
            }

            System.Reflection.PropertyInfo[] propertiesInfo = source.GetType().GetProperties();

            Parallel.ForEach(propertiesInfo, item =>
            {
                var targetProperty = target.GetType().GetProperty(item.Name);

                if (targetProperty != null)
                {
                    targetProperty.SetValue(target, item.GetValue(source, null), null);
                }
            });
        }
#endif
    }
}
