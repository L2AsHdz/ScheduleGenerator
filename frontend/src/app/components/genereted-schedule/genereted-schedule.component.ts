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

    constructor(private service: ScheduleService, private route: Router) { }

    ngOnInit(): void {
        let horario = localStorage.getItem('horario');
        let horarioData = horario ? JSON.parse(horario) : null;
        this.responseData = horarioData;
    }

    getHorarioCompleto(): CursoHorarioDto[] {
        // Crear un nuevo array para almacenar los resultados
        const resultados: CursoHorarioDto[] = [];

        // Iterar a través de todas las horas y salones
        for (const hora of this.generarIteraciones()) {
            for (const salon of this.responseData.salones) {
                // Verificar si hay un CursoAsignado correspondiente en el array original
                const cursoAsignadoExistente = this.responseData.horario.find(curso => {
                    return this.getHora(curso.horaInicio) == hora.horaInicio && curso.salon.noSalon == salon.noSalon;
                });

                // Si se encuentra un CursoAsignado existente, agregarlo a los resultados
                if (cursoAsignadoExistente) {
                    resultados.push(cursoAsignadoExistente);
                } else {
                    // Si no se encuentra, crear un objeto CursoAsignado vacío y agregarlo
                    const cursoAsignadoVacio: CursoHorarioDto = {
                        idRegistro: 0,
                        codigoHorario: 0,
                        curso: {
                            codigoCurso: 0,
                            nombre: '',
                            semestre: 0,
                            carrera: {
                                codigoCarrera: 0,
                                nombre: '',
                                cantidadSemestres: 0,
                                presupuesto: 0,
                                color: '',
                            },
                        },
                        catedratico: {
                            codigoCatedratico: 0,
                            nombre: '',
                            noColegiado: 0,
                            horaEntrada: '',
                            horaSalida: '',
                        },
                        salon: salon,
                        horaInicio: hora.horaInicio,
                        horaFin: ''
                    };
                    resultados.push(cursoAsignadoVacio);
                }
            }
        }
        return resultados;
    }

    generarIteraciones(): Iteracion[] {
        const iteraciones: Iteracion[] = [];

        const fechaInicio = new Date(`2023-01-01T${this.responseData.horaInicio}`);
        const fechaFin = new Date(`2023-01-01T${this.responseData.horaFin}`);

        while (fechaInicio < fechaFin) {
            let horaInicio = fechaInicio.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
            let horaFin = fechaInicio.setMinutes(fechaInicio.getMinutes() + this.responseData.duracionPeriodo);
            let horaFin2 = fechaFin.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
            iteraciones.push({ horaInicio: horaInicio, horaFin: horaFin2 });
        }
        return iteraciones;
    }

    getHora(time: string): string {
        const fecha = new Date(`2023-01-01T${time}`);
        let hora = fecha.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
        return hora;
    }

    getCursos(hora: string): CursoHorarioDto[] {
        return this.getHorarioCompleto().filter(curso => {
            return this.getHora(curso.horaInicio) == hora;
        });
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
            advertencias: this.responseData.advertencias
        };


        this.service.guardarHorario(data).subscribe(data => {
            // Swal.close();
            // Swal.fire({
            //     title: 'Horario guardado',
            //     text: 'El horario se ha guardado correctamente',
            //     icon: 'success',
            //     confirmButtonText: 'Aceptar'
            // }).then((result) => {
                this.route.navigate(['/history']);
            // });
        });
    }
}

interface Iteracion {
    horaInicio: string;
    horaFin: string;
}
