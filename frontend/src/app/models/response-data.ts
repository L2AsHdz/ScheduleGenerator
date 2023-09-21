import { Carrera } from "./carrera";
import { CursoAdvertencia } from "./curso-advertencia";
import { CursoHorarioDto } from "./curso-horario-dto";
import { Salon } from "./salon";

export interface ResponseData {
    horario: CursoHorarioDto[];
    advertencias: CursoAdvertencia[];
    carreras: Carrera[];
    salones : Salon[];
    horaInicio: string;
    horaFin: string;
    duracionPeriodo: number;
    porcentajeEfectividad: number;
}
