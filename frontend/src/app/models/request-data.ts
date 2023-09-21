import { CursoAdvertencia } from "./curso-advertencia";
import { CursoRequest } from "./curso-request";

export interface RequestData {
    horario: CursoRequest[];
    advertencias: CursoAdvertencia[];
}
