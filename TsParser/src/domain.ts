
export interface SourceModel {
    path: string;
    classes: ClassModel[];
    enums: EnumModel[];
    interfaces: InterfaceModel[];
}

export interface ClassModel {
    name: string;
    properties: PropertyModel[];
    methods: MethodModel[];
    constructors: ConstructorModel[];
    modifier : ModifierKind;
    language : Language;
}

export interface ConstructorModel {
    name: string;
    parameters: ParameterModel[];
    modifier : ModifierKind;
}

export interface PropertyModel {
    name: string;
    type: string | null;
    modifier : ModifierKind;
}

export interface MethodModel {
    name: string;
    parameters: ParameterModel[];
    returnType: string;
    modifier : ModifierKind;
}

export interface ParameterModel {
    name: string;
    type: string;
}

export interface InterfaceModel {
    name: string;
    properties: PropertyModel[];
    methods: MethodModel[];
    modifier : ModifierKind;
    language : Language;
}


export interface EnumModel {
    name: string;
    values: EnumValueModel[];
    modifier : ModifierKind;
    language : Language;
}


export interface EnumValueModel {
    name: string;
    value: string;
}

export enum ModifierKind {
    Public,
    Private,
    Protected,
    Internal,
    Static,
}

export enum Language
{
    CSharp,
    TypeScript
}