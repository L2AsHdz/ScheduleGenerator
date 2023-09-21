import { CursoAdvertencia } from "./curso-advertencia";
import { CursoRequest } from "./curso-request";

export interface RequestData {
    horario: CursoRequest[];
    advertencias: CursoAdvertencia[];
    porcentajeEfectividad: number;
    comentario: string;
    horaInicio: string;
    horaFin: string;
    duracionPeriodo: number;
}
