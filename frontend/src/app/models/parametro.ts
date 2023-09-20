export interface Parametro {
    codigoParametro: number;
    nombre: string;
    valor: string;
    tipo: TipoParametro;
    descripcion: string;
}

export enum TipoParametro {
    CONFIGURACION = 'CONFIGURACION',
    PRIORIDAD = 'PRIORIDAD',
    COMPORTAMIENTO = 'COMPORTAMIENTO',
}
