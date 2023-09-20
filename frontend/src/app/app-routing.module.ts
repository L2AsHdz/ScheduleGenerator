import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { ConfigComponent } from './components/config/config.component';
import { SchedulerComponent } from './components/scheduler/scheduler.component';
import { ManagerRoutesComponent } from './components/manager-routes/manager-routes.component';
import { GeneretedScheduleComponent } from './components/genereted-schedule/genereted-schedule.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'config', component: ConfigComponent },
    { path: 'scheduler', component: ManagerRoutesComponent,
        children: [
            { path: 'generate', component: SchedulerComponent },
            { path: 'view', component: GeneretedScheduleComponent }
        ] }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
