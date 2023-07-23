import { ClassModel, EnumModel, InterfaceModel, SourceModel, PropertyModel, MethodModel, ParameterModel, ConstructorModel } from "./domain.js";

export function getSkeleton(source: SourceModel): string {

    const classes = source.classes.map(getClassSkeleton);
    const interfaces = source.interfaces.map(getInterfaceSkeleton);
    const enums = source.enums.map(getEnumSkeleton);

    throw new Error("Not implemented");
}

function getClassSkeleton(classDeclaration: ClassModel): string {

    const properties = classDeclaration.properties.map(getPropertySkeleton);
    const methods = classDeclaration.methods.map(getMethodSkeleton);
    const constructors = classDeclaration.constructors.map(getConstructorSkeleton);
    return `\\class ${classDeclaration.name} {\n\n ${properties.join("\n")} \n\n${constructors.join("\n")}\n\n${methods.join("\n")}}`;
}

function getConstructorSkeleton(constructorModel: ConstructorModel): string {

    throw new Error("Not implemented");
}

function getPropertySkeleton(property: PropertyModel): string {
    return `\t//${property.isStatic? "static": ""} ${property.name}: ${property.type};`;
}

function getMethodSkeleton(method: MethodModel): string {

    const parameters = method.parameters.map(getParameterSkeleton);
    return `\t//${method.isStatic? "static": ""} ${method.name}(${parameters.join(", ")}): ${method.returnType};`;
}

function getInterfaceSkeleton(interfaceDeclaration: InterfaceModel): string {

    const properties = interfaceDeclaration.properties.map(getPropertySkeleton);
    const methods = interfaceDeclaration.methods.map(getMethodSkeleton);

    return`interface ${interfaceDeclaration.name} {
        ${properties.join("\n")}

        \t//methods

        ${methods.join("\n")}\n
    }`;
    
}

function getEnumSkeleton(enumDeclaration: EnumModel): string {

    const values = enumDeclaration.values.map((value) => `\t${value} = "${value}"`);

    throw new Error("Not implemented");
}

function getParameterSkeleton(parameter: ParameterModel): string {
    
        throw new Error("Not implemented");
    }