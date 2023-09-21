import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseData } from '../models/response-data';
import { Observable } from 'rxjs';
import { CursoHorarioDto } from '../models/curso-horario-dto';
import { RequestData } from '../models/request-data';

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

    guardarHorario(data: RequestData) {
        return this.http.post<RequestData>(this._urlBase, data);
    }

    setHorario(horario: ResponseData) {
        this.horario = horario;
    }

    getHorario(): ResponseData {
        return this.horario;
    }
}
