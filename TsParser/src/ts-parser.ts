import ts from "typescript";
import fs from "fs";
import { ClassModel, EnumModel, EnumValueModel, InterfaceModel, MethodModel, ParameterModel, PropertyModel, ConstructorModel, SourceModel } from "./domain.js";

export function parseSource(fullPath: string): SourceModel {

    const fileName =  fullPath.split("\\").pop() ?? "no-name";
    const code = fs.readFileSync(fullPath).toString();
    const sourceFile = ts.createSourceFile(fileName, code, ts.ScriptTarget.Latest, true);

    const classes = getAllClassesRecursive(sourceFile);
    const enums = getAllEnumsRecursive(sourceFile);
    const interfaces = getAllInterfacesRecursive(sourceFile);

    const parsedClasses = classes.map(parseClass);
    const parsedEnums = enums.map(parseEnum);
    const parsedInterfaces = interfaces.map(parseInterface);

    return {
        path: fullPath,
        classes: parsedClasses,
        enums: parsedEnums,
        interfaces: parsedInterfaces
    };
}

function parseClass(classDeclaration: ts.ClassDeclaration): ClassModel {

    const name = classDeclaration.name?.getText() ?? "no-name";
    const propertiesDeclaration = classDeclaration.members
        .filter((member) => ts.isPropertyDeclaration(member)
            && member.modifiers?.some((md) => md.kind == ts.SyntaxKind.PublicKeyword)) as any as ts.PropertyDeclaration[];

    const methodsDeclaration = classDeclaration.members.
        filter((member) => ts.isMethodDeclaration(member) &&
            member.modifiers?.some((md) => md.kind == ts.SyntaxKind.PublicKeyword)) as any as ts.MethodDeclaration[]

    const constructorParametersDeclaration = classDeclaration.members.
        filter((member) => ts.isConstructorDeclaration(member)) as any as ts.ConstructorDeclaration[]

    const constructors = constructorParametersDeclaration.map(parseConstructorParameter);
    
    const properties = propertiesDeclaration.map(parseProperty);
    const methods = methodsDeclaration.map(parseMethod);

    return {
        name: name,
        properties: properties,
        methods: methods,
        constructors: constructors
    };
}

function parseConstructorParameter(constructorDeclaration: ts.ConstructorDeclaration): ConstructorModel {

    const parameters = constructorDeclaration.parameters.map(parseParameter);
    const name = constructorDeclaration.name?.getText() ?? "no-name";
    return {
        name: name,
        parameters: parameters
    };
}

function parseParameter(parameterDeclaration: ts.ParameterDeclaration): ParameterModel {

    const name = parameterDeclaration.name?.getText() ?? "no-name";
    const type = parameterDeclaration.type?.getText() ?? "any";

    return {
        name: name,
        type: type
    };
}

function parseEnum(enumDeclaration: ts.EnumDeclaration): EnumModel {

    const name = enumDeclaration.name?.getText() ?? "no-name";
    const valuesDeclaration = enumDeclaration.members;
    const values = valuesDeclaration.map(parseEnumValue);

    return {
        name: name,
        values: values
    };
}

function parseInterface(interfaceDeclaration: ts.InterfaceDeclaration): InterfaceModel {

    const name = interfaceDeclaration.name?.getText() ?? "no-name";
    const propertiesDeclaration = interfaceDeclaration.members.
        filter((member) => ts.isPropertySignature(member)) as any as ts.PropertySignature[]

    const methodsDeclaration = interfaceDeclaration.members
        .filter((member) => ts.isMethodSignature(member)) as any as ts.MethodSignature[];

    const properties = propertiesDeclaration.map(parseProperty);
    const methods = methodsDeclaration.map(parseMethod);

    return {
        name: name,
        properties: properties,
        methods: methods
    };
}

function parseProperty(propertyDeclaration: ts.PropertyDeclaration | ts.PropertySignature): PropertyModel {

    const name = propertyDeclaration.name?.getText() ?? "no-name";
    const type = propertyDeclaration.type?.getText() ?? "any";
    const isStatic = propertyDeclaration.modifiers?.some((modifier) => modifier.kind === ts.SyntaxKind.StaticKeyword) ?? false;

    return {
        name: name,
        type: type,
        isStatic: isStatic
    };
}

function parseMethod(methodDeclaration: ts.MethodDeclaration | ts.MethodSignature): MethodModel {

    const name = methodDeclaration.name?.getText() ?? "no-name";
    const parameters = methodDeclaration.parameters.map(parseParameter);
    const returnType = methodDeclaration.type?.getText() ?? "any";
    const isStatic = methodDeclaration.modifiers?.some((modifier) => modifier.kind === ts.SyntaxKind.StaticKeyword) ?? false;

    return {
        name: name,
        parameters: parameters,
        returnType: returnType,
        isStatic: isStatic
    };
}

function parseEnumValue(enumMember: ts.EnumMember): EnumValueModel {

    const name = enumMember.name?.getText() ?? "no-name";
    const value = enumMember.initializer?.getText() ?? "no-value";
    return {
        name: name,
        value: value
    };
}

function getAllClassesRecursive(sourceFile: ts.SourceFile): ts.ClassDeclaration[] {
    const allClasses = new Array<ts.ClassDeclaration>();
    getAllClassesRecursiveInternal(allClasses, sourceFile);
    return allClasses;
}

function getAllEnumsRecursive(sourceFile: ts.SourceFile): ts.EnumDeclaration[] {
    const allEnums = new Array<ts.EnumDeclaration>();
    getAllEnumsRecursiveInternal(allEnums, sourceFile);
    return allEnums;
}

function getAllInterfacesRecursive(sourceFile: ts.SourceFile): ts.InterfaceDeclaration[] {
    const allInterfaces = new Array<ts.InterfaceDeclaration>();
    getAllInterfacesRecursiveInternal(allInterfaces, sourceFile);
    return allInterfaces;
}

function getAllClassesRecursiveInternal(allClasses: ts.ClassDeclaration[], node: ts.Node): void {

    if (ts.isClassDeclaration(node) &&
        node.modifiers?.some((md) => md.kind == ts.SyntaxKind.ExportKeyword)) {
        allClasses.push(node);
    }
    node.forEachChild(child => getAllClassesRecursiveInternal(allClasses, child));
}

function getAllEnumsRecursiveInternal(allEnums: ts.EnumDeclaration[], node: ts.Node): void {

    if (ts.isEnumDeclaration(node) &&
        node.modifiers?.some((md) => md.kind == ts.SyntaxKind.ExportKeyword)) {
        allEnums.push(node);
    }
    node.forEachChild(child => getAllEnumsRecursiveInternal(allEnums, child));
}


function getAllInterfacesRecursiveInternal(allInterfaces: ts.InterfaceDeclaration[], node: ts.Node): void {

    if (ts.isInterfaceDeclaration(node) &&
        node.modifiers?.some((md) => md.kind == ts.SyntaxKind.ExportKeyword)) {
        allInterfaces.push(node);
    }
    node.forEachChild(child => getAllInterfacesRecursiveInternal(allInterfaces, child));
}
