using System.Linq;
using System.Threading.Tasks;

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
    }
}
