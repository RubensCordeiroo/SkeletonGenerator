using SkeletonGenerator.Domain;

namespace SkeletonGenerator
{
    public static class SkeletonModelFactory
    {
        public static List<SkeletonModel> GetSkeletons(Dictionary<string, List<TypeModel>> sourcesType,
                                                      Language target,
                                                      Language other)
        {
            var skeletons = new List<SkeletonModel>();
            foreach (var sourceType in sourcesType)
            {
                var types = sourceType.Value;
                var skeleton = Create(types, target, other);
                skeletons.Add(skeleton);
            }
            return skeletons;
        }

        private static SkeletonModel Create(List<TypeModel> types, 
                                            Language target,
                                            Language other)
        {
            //force validation
            var name = types.Distinct().Single().Name;
            var type = types.Distinct().Single().Type;
            var modifier = GetBetterModifier(types.Select(x => x.Modifier));

            var targetType = types.Where(x => x.Language == target).FirstOrDefault();
            var otherType = types.Where(x => x.Language == other).FirstOrDefault();

            var targetProperties = targetType?.Properties;
            var orderProperties = targetProperties!= null 
                                    ? otherType?.Properties.Except(targetProperties)
                                    : otherType?.Properties;

            var targetMethods = targetType?.Methods;
            var orderMethods = targetMethods != null
                                    ? otherType?.Methods.Except(targetMethods)
                                    : otherType?.Methods;

            var targetConstructors = targetType?.Constructors;
            var orderConstructors = targetConstructors != null
                                    ? otherType?.Constructors.Except(targetConstructors)
                                    : otherType?.Constructors;

            return new SkeletonModel
            {
                Name = name,
                Modifier = modifier,
                Type = type,
                TargetLanguage = target,
                OtherLanguage = other,
                TargetProperties = targetProperties,
                OtherProperties = orderProperties?.ToList(),
                TargetMethods = GetMethods(types),
                OtherMethods = orderMethods?.ToList(),
                TargetConstructors = GetConstructors(types),
                OtherConstructors = orderConstructors?.ToList(),
            };
        }

       

        private static List<ConstructorModel> GetConstructors(IEnumerable<TypeModel> types) 
        {
            return types.SelectMany(x => x.Constructors).Distinct().ToList();
        }
        private static List<PropertyModel> GetProperties(IEnumerable<TypeModel> types)
        {
            return types.SelectMany(x => x.Properties).Distinct().ToList();
        }
        private static List<MethodModel> GetMethods(IEnumerable<TypeModel> types) 
        {
            return types.SelectMany(x => x.Methods).Distinct().ToList();
        }
        private static List<EnumValueModel> GetEnumValues(IEnumerable<EnumModel> enuns)
        {
            return enuns.SelectMany(x => x.Values).Distinct().ToList();
        }


        private static ModifierKind GetBetterModifier(IEnumerable<ModifierKind> modifiers)
        {
            if (modifiers.Any(x => x == ModifierKind.Public))
            {
                return ModifierKind.Public;
            }

            if (modifiers.Any(x => x == ModifierKind.Protected))
            {
                return ModifierKind.Protected;
            }

            if (modifiers.Any(x => x == ModifierKind.Internal))
            {
                return ModifierKind.Internal;
            }

            if (modifiers.Any(x => x == ModifierKind.Private))
            {
                return ModifierKind.Private;
            }
            return ModifierKind.None;
        }
    }
}
