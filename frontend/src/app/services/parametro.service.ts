import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Parametro } from '../models/parametro';

@Injectable({
    providedIn: 'root'
})
export class ParametroService {

    private _urlBase = 'https://localhost:7030/scheduleAPI/Parametro';

    constructor(private http: HttpClient) { }

    public getParametros(): Observable<Parametro[]> {
        return this.http.get<Parametro[]>(this._urlBase);
    }

    public updateParametro(parametro: Parametro): Observable<Parametro> {
        return this.http.put<Parametro>(`${this._urlBase}/${parametro.codigoParametro}`, parametro);
    }
}
