import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ResponseData } from 'src/app/models/response-data';
import { ScheduleService } from 'src/app/services/schedule.service';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-historial',
    templateUrl: './historial.component.html',
    styleUrls: ['./historial.component.css']
})
export class HistorialComponent implements OnInit {

    responseData: ResponseData;
    idhorario: number = 0;
    lastHorario: number = 0;

    constructor(private service: ScheduleService, private route: ActivatedRoute, private router: Router) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe(params => {
            const id = this.route.snapshot.paramMap.get('id') ?? '0';
            this.idhorario = Number.parseInt(id);
            this.lastHorario = Number.parseInt(localStorage.getItem('lastHorario') ?? '0');

            this.service.getHorarioById(Number.parseInt(id))
                .subscribe(data => {
                    this.responseData = data;
                });
        });
    }

    next() {
        let id = this.idhorario + 1;

        if (id <= this.lastHorario) {
            this.service.getHorarioById(Number.parseInt(id + ''))
                .subscribe(data => {
                    this.responseData = data;
                });
            this.router.navigate(['/history', id]);
        }
        else {
            Swal.fire({
                title: 'Este es el ultimo horario generado',
                icon: 'warning',
                showCancelButton: false,
                confirmButtonText: 'Ok',
            });
        }
    }

    prev() {
        let id = this.idhorario - 1;

        if (id > 0) {
            this.service.getHorarioById(Number.parseInt(id + ''))
                .subscribe(data => {
                    this.responseData = data;
                });
            this.router.navigate(['/history', id]);
        }
        else {
            Swal.fire({
                title: 'Este es el primer horario',
                icon: 'warning',
                showCancelButton: false,
                confirmButtonText: 'Ok',
            });
        }

    }

}
