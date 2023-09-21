import { Catedratico } from "./catedratico";
import { CursoDto } from "./curso-dto";
import { Salon } from "./salon";

export interface CursoHorarioDto {
    idRegistro: number;
    codigoHorario: number;
    curso: CursoDto;
    catedratico: Catedratico;
    salon: Salon;
    horaInicio: string;
    horaFin: string;
}
