
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
}

export interface ConstructorModel {
    name:string;
    parameters: ParameterModel[];
}

export interface PropertyModel {
    name: string;
    type: string | null;
    isStatic : boolean;
}

export interface MethodModel {
    name: string;
    parameters: ParameterModel[];
    returnType: string;
    isStatic : boolean;
}

export interface ParameterModel {
    name: string;
    type: string;
}

export interface EnumModel {
    name: string;
    values: EnumValueModel[];
}

export interface InterfaceModel {
    name: string;
    properties: PropertyModel[];
    methods: MethodModel[];
}

export interface EnumValueModel {
    name: string;
    value: string;
}