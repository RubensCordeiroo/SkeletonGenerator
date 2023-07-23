namespace SkeletonGenerator.Domain;

public class SourcesModel
{
    public SourceModel? SourceTs { get; init; }
    public SourceModel? SourceCSharp { get; init; }
}
 
public class SourceModel
{
    public required string Path { get; set; }
    public required List<ClassModel> Classes { get; init; }
    public required List<EnumModel> Enums { get; init; }
    public required List<InterfaceModel> Interfaces { get; init; }
}

public class ClassModel
{
    public required string Name { get; init; }
    public required List<PropertyModel> Properties { get; init; }
    public required List<MethodModel> Methods { get; init; }
    public required List<ConstructorModel> Constructors { get; init; }
}

public class ConstructorModel
{
    public required string Name { get; init; }
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
    public bool IsStatic { get; init; }

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
    public required List<ParameterModel> Parameters { get; init; }
    public required string ReturnType { get; init; }
    public bool IsStatic { get; init; }

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

public class EnumModel
{
    public required string Name { get; init; }
    public required List<EnumValueModel> Values { get; init; }

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

public class InterfaceModel
{
    public required string Name { get; init; }
    public required List<PropertyModel> Properties { get; init; }
    public required List<MethodModel> Methods { get; init; }

    public override bool Equals(object? obj)
    {
        if(obj is InterfaceModel model)
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
