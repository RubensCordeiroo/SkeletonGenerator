using SkeletonGenerator.Domain;
using System.Collections.Generic;
using System.Text;

namespace SkeletonGenerator
{
    public class SkeletonModelParser
    {
        internal static string Parse(SkeletonModel skeleton)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"//export {skeleton.Modifier.ToString().ToLower()} {skeleton.Type.ToString().ToLower()} {skeleton.Name} {{");


            var targetSkeleton = GetContent(skeleton.TargetProperties,
                                           skeleton.TargetMethods, 
                                           skeleton.TargetConstructors, 
                                           skeleton.TargetValues);

            var ortherSkeleton = GetContent(skeleton.OtherProperties,
                                            skeleton.OtherMethods,
                                            skeleton.OtherConstructors,
                                            skeleton.OtherValues);

            if (!String.IsNullOrWhiteSpace(targetSkeleton))
            {
                sb.AppendLine(targetSkeleton.ToString());
            }

            if (!String.IsNullOrWhiteSpace(ortherSkeleton))
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine($"    //{skeleton.OtherLanguage} language");
                sb.AppendLine(ortherSkeleton);
            }

            sb.AppendLine($"//}}");
            return sb.ToString();
        }

        private static string GetContent(List<PropertyModel>? properties, 
                                         List<MethodModel>? methods, 
                                         List<ConstructorModel>? constructors, 
                                         List<EnumValueModel>? values)
        {
            var sb = new StringBuilder();
            if (properties is not null)
            {
                foreach (var property in properties.OrderBy(x=> x.Name))
                {
                    sb.AppendLine($"    //{property.Modifier.ToString().ToLower()} {property.Name}: {property.Type};");
                }
            }

            if (constructors is not null)
            {
                foreach (var constructor in constructors.OrderBy(x => x.Name))
                {
                    sb.AppendLine($"    //{constructor.Modifier.ToString().ToLower()} constructor({GetParameters(constructor.Parameters)});");
                }
            }

            if (methods is not null)
            {
                foreach (var method in methods.OrderBy(x => x.Name))
                {
                    sb.AppendLine($"    //{method.Modifier.ToString().ToLower()} {method.Name}({GetParameters(method.Parameters)}): {method.ReturnType};");
                }
            }

            if (values is not null)
            {
                foreach (var value in values)
                {
                    sb.AppendLine($"    //{value.Name} = {value.Value},");
                }
            }
            return sb.ToString();
        }

        private static object GetParameters(List<ParameterModel> parameters)
        {
            return string.Join(", ", parameters.Select(x => $"{x.Name}: {x.Type}"));
        }
    }
}
