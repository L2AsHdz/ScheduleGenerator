import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CursoHorarioDto } from 'src/app/models/curso-horario-dto';
import { CursoRequest } from 'src/app/models/curso-request';
import { RequestData } from 'src/app/models/request-data';
import { ResponseData } from 'src/app/models/response-data';
import { ScheduleService } from 'src/app/services/schedule.service';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-genereted-schedule',
    templateUrl: './genereted-schedule.component.html',
    styleUrls: ['./genereted-schedule.component.css']
})
export class GeneretedScheduleComponent implements OnInit {

    responseData: ResponseData;
    comentario: string = '';

    constructor(private service: ScheduleService, private route: Router) { }

    ngOnInit(): void {
        let horario = localStorage.getItem('horario');
        let horarioData = horario ? JSON.parse(horario) : null;
        this.responseData = horarioData;
    }

    guardarEnBase() {
        // Swal.fire({
        //     title: 'Guardando horario',
        //     allowEscapeKey: false,
        //     allowOutsideClick: false,
        //     showConfirmButton: false
        // });
        // Swal.showLoading();

        let horarioRequest: CursoRequest[] = [];

        this.responseData.horario.forEach(curso => {
            let cursoRequest: CursoRequest = {
                codigoHorario: curso.codigoHorario,
                codigoCurso: curso.curso.codigoCurso,
                codigoCatedratico: curso.catedratico.codigoCatedratico,
                codigoCarrera: curso.curso.carrera.codigoCarrera,
                noSalon: curso.salon.noSalon,
                horaInicio: curso.horaInicio,
                horaFin: curso.horaFin
            };
            horarioRequest.push(cursoRequest);
        });

        let data: RequestData = {
            horario: horarioRequest,
            advertencias: this.responseData.advertencias,
            porcentajeEfectividad: this.responseData.porcentajeEfectividad,
            comentario: this.comentario,
            horaFin: this.responseData.horaFin,
            horaInicio: this.responseData.horaInicio,
            duracionPeriodo: this.responseData.duracionPeriodo
        };


        this.service.guardarHorario(data).subscribe(data => {
            // Swal.close();
            // Swal.fire({
            //     title: 'Horario guardado',
            //     text: 'El horario se ha guardado correctamente',
            //     icon: 'success',
            //     confirmButtonText: 'Aceptar'
            // }).then((result) => {
            localStorage.setItem('lastHorario', data.codigoHorario + '');
            this.route.navigate(['/history', data.codigoHorario]);
            // });
        });
    }
}

interface Iteracion {
    horaInicio: string;
    horaFin: string;
}
