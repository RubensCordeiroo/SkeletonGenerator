namespace SkeletonGenerator.Domain;
internal class SourcesModel
{
    public SourceModel? SourceTs { get; set; }
    public SourceModel? SourceCSharp { get; set; }
    public List<TypeModel> SourceTypes
    {
        get
        {
            var types = new List<TypeModel>();
            if (SourceTs is not null)
            {
                types.AddRange(SourceTs.SourceTypes);
            }
            if (SourceCSharp is not null)
            {
                types.AddRange(SourceCSharp.SourceTypes);
            }
            return types;
        }
    }
}
public class SourceModel
{
    public required string Path { get; set; }
    public required List<ClassModel> Classes { get; init; }
    public required List<EnumModel> Enums { get; init; }
    public required List<InterfaceModel> Interfaces { get; init; }

    public TypeModel[] SourceTypes => Classes.Cast<TypeModel>()
                                             .Concat(Enums)
                                             .Concat(Interfaces)
                                             .ToArray();
}

public abstract class TypeModel
{
    public required string Name { get; init; }
    public required ModifierKind Modifier { get; init; }
    public required Language Language { get; init; }
    public List<PropertyModel> Properties { get; init; } = new();
    public List<MethodModel> Methods { get; init; } = new();
    public List<ConstructorModel> Constructors { get; init; } = new();
    public List<EnumValueModel> Values { get; init; } = new();
    public abstract EnumTypeModel Type { get; }

}


public class ClassModel : TypeModel
{
    public override EnumTypeModel Type => EnumTypeModel.Class;
    public override bool Equals(object? obj)
    {
        if (obj is ClassModel model)
        {
            return this.Name == model.Name;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }


}

public class InterfaceModel : TypeModel
{
    public override EnumTypeModel Type => EnumTypeModel.Interface;

    public override bool Equals(object? obj)
    {
        if (obj is InterfaceModel model)
        {
            return this.Name == model.Name;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}

public class EnumModel : TypeModel
{
    public override EnumTypeModel Type => EnumTypeModel.Enum;

    public override bool Equals(object? obj)
    {
        if (obj is EnumModel other)
        {
            return other.Name == Name;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}

public class ConstructorModel
{
    public required string Name { get; init; }
    public required ModifierKind Modifier { get; init; }
    public required List<ParameterModel> Parameters { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is ConstructorModel other)
        {
            return other.Name == Name &&
                   other.Parameters.Count == Parameters.Count &&
                   other.Parameters.All(x => Parameters.Contains(x));
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Parameters.GetHashCode();
    }
}

public class PropertyModel
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public required ModifierKind Modifier { get; init; }
    public override bool Equals(object? obj)
    {
        if (obj is PropertyModel other)
        {
            return other.Name == Name &&
                    other.Type == Type;
        }
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Type.GetHashCode();
    }
}

public class MethodModel
{
    public required string Name { get; init; }
    public required ModifierKind Modifier { get; init; }
    public required string ReturnType { get; init; }
    public required List<ParameterModel> Parameters { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is MethodModel other)
        {
            return other.Name == Name &&
                    other.ReturnType == ReturnType &&
                    other.Parameters.Count == Parameters.Count &&
                    other.Parameters.All(x => Parameters.Contains(x));
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ ReturnType.GetHashCode() ^ Parameters.GetHashCode();
    }
}

public class ParameterModel
{
    public required string Name { get; init; }
    public required string Type { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is ParameterModel other)
        {
            return other.Name == Name &&
                   other.Type == Type;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Type.GetHashCode();
    }
}

public class EnumValueModel
{
    public required string Name { get; init; }
    public required string Value { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is EnumValueModel other)
        {
            return other.Name == Name &&
                   other.Value == Value;
        }
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Value.GetHashCode();
    }
}


public enum ModifierKind
{
    Public,
    Private,
    Protected,
    Internal,
    Static,
    None
}

public enum EnumTypeModel
{
    Class,
    Interface,
    Enum
}

public enum Language
{
    CSharp,
    TypeScript
}