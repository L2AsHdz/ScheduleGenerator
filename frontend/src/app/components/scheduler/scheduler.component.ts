import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ScheduleService } from 'src/app/services/schedule.service';

@Component({
    selector: 'app-scheduler',
    templateUrl: './scheduler.component.html',
    styleUrls: ['./scheduler.component.css']
})
export class SchedulerComponent {

    constructor(private scheduleService: ScheduleService, private route: Router) { }

    generateSchedule() {

        this.scheduleService.generateSchedule()
            .subscribe(data => {
                this.scheduleService.setHorario(data as any[]);
                this.route.navigate(['/scheduler/view']);
            });
    }
}
