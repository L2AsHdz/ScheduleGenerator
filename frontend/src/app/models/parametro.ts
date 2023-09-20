export interface Parametro {
    codigoParametro: number;
    nombre: string;
    valor: string;
    tipo: TipoParametro;
    descripcion: string;
}

export enum TipoParametro {
    CONFIGURACION = 0,
    PRIORIDAD = 1,
    COMPORTAMIENTO = 2,
}
