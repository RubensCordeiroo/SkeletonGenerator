public class Source
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
}


public class PropertyModel
{
    public required string Name { get; init; }
    public required string Type { get; init; }
    public bool IsStatic { get; init; }
}

public class MethodModel
{
    public required string Name { get; init; }
    public required List<ParameterModel> Parameters { get; init; }
    public required string ReturnType { get; init; }
    public bool IsStatic { get; init; }
}

public class ParameterModel
{
    public required string Name { get; init; }
    public required string Type { get; init; }
}

public class EnumModel
{
    public required string Name { get; init; }
    public required List<EnumValueModel> Values { get; init; }
}

public class InterfaceModel
{
    public required string Name { get; init; }
    public required List<PropertyModel> Properties { get; init; }
    public required List<MethodModel> Methods { get; init; }
}

public class EnumValueModel
{
    public required string Name { get; init; }
    public required string Value { get; init; }
}
