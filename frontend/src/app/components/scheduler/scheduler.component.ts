import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ScheduleService } from 'src/app/services/schedule.service';
import Swal from 'sweetalert2';

@Component({
    selector: 'app-scheduler',
    templateUrl: './scheduler.component.html',
    styleUrls: ['./scheduler.component.css']
})
export class SchedulerComponent {

    constructor(private scheduleService: ScheduleService, private route: Router) { }

    async generateSchedule() {
        // Swal.fire({
        //     title: 'Generando horario',
        //     allowEscapeKey: false,
        //     allowOutsideClick: false,
        //     showConfirmButton: false,
        // });
        // Swal.showLoading();


        this.scheduleService.generateSchedule()
            .subscribe(data => {
                // Swal.close();
                localStorage.setItem('horario', JSON.stringify(data));
                this.route.navigate(['/scheduler/view']);
            });
    }
}
