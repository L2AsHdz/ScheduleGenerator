import { Carrera } from "./carrera";

export interface CursoDto {
    codigoCurso: number;
    nombre: string;
    semestre: number;
    carrera: Carrera;
}
