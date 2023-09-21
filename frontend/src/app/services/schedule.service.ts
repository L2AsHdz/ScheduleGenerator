import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseData } from '../models/response-data';
import { Observable } from 'rxjs';
import { CursoHorarioDto } from '../models/curso-horario-dto';
import { RequestData } from '../models/request-data';
import { Horario } from '../models/horario';

@Injectable({
    providedIn: 'root'
})
export class ScheduleService {

    private _urlBase = 'https://localhost:7030/scheduleAPI/Schedule';

    private horario: ResponseData;

    constructor(private http: HttpClient) { }

    generateSchedule(): Observable<ResponseData> {
        return this.http.get<ResponseData>(this._urlBase);
    }

    guardarHorario(data: RequestData): Observable<Horario> {
        return this.http.post<Horario>(this._urlBase, data);
    }

    getHorarioById(id: number): Observable<ResponseData> {
        return this.http.get<ResponseData>(`${this._urlBase}/${id}`);
    }

    setHorario(horario: ResponseData) {
        this.horario = horario;
    }

    getHorario(): ResponseData {
        return this.horario;
    }
}
