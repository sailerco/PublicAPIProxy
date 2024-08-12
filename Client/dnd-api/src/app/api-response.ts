export interface ApiResponse<T> {
    count: number;
    results: T[];
}

export interface DndResource{
    index: string;
    level?: string
    name: string;
    url?: string;
}
export type DndResourceResponse = ApiResponse<DndResource>;

export interface SpellList{
    index: string;
    level: string;
    name: string;
    url: string;
}
export type SpellListResponse = ApiResponse<SpellList>;

export interface SpellDetailsResponse{
    index?: string;
    level?: string;
    name?: string;
    url?: string;
    desc?: string[];
    higher_level?: string[];
    range?: string;
    components?: string[];
    ritual?: string;
    duation?: string;
    concentration?: string;
    casting_time?: string;
    attack_type?: string;
    damage?: string;
    school?: DndResource;
    subclasses?: DndResource;
}

