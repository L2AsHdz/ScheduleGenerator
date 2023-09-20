import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ScheduleService {

    private _urlBase = 'https://localhost:7030/scheduleAPI/Schedule';

    private horario: any[];

    constructor(private http: HttpClient) { }

    generateSchedule() {
        return this.http.get(this._urlBase);
    }

    setHorario(horario: any[]) {
        this.horario = horario;
    }

    getHorario() {
        return this.horario;
    }
}
