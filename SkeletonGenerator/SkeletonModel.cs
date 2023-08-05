using SkeletonGenerator.Domain;

namespace SkeletonGenerator
{
    public class SkeletonModel
    {
        public required string Name { get; init; }
        public required ModifierKind Modifier { get; init; }
        public required EnumTypeModel Type { get; init; }
        public required Language TargetLanguage { get; init; }
        public required Language OtherLanguage { get; init; }
        public List<PropertyModel>? TargetProperties { get; init; }
        public List<MethodModel>? TargetMethods { get; init; }
        public List<ConstructorModel>? TargetConstructors { get; init; }

        public List<PropertyModel>? OtherProperties { get; init; }
        public List<MethodModel>? OtherMethods { get; init; }
        public List<ConstructorModel>? OtherConstructors { get; init; }

        public List<EnumValueModel>? TargetValues { get; init; }

        public List<EnumValueModel>? OtherValues { get; init; }

        public override bool Equals(object? obj)
        {
            return obj is SkeletonModel model 
                          ? Name == model.Name 
                          : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
