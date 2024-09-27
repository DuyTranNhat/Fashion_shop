export type AttributeGet = {
    attributeId: number,
    name: string,
    status: true,
    values: ValueGet[]
}

export type ValueGet = {
    valueId: number,
    value1: string,
    status: boolean
}

export type AttributeUpdate = {
    name: string,
    values: VallueUpdate[]
}


export type VallueUpdate = {
    valueId: number,
    value1: string,
    status: boolean
    
}

export type AttributePost = {
    name: string,
    values: ValuePost[]
}

export type ValuePost = {
    value1: string,
}


