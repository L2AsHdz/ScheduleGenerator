import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { ConfigComponent } from './components/config/config.component';
import { FormsModule } from '@angular/forms';
import { FilterPipe } from './pipes/filter.pipe';
import { SchedulerComponent } from './components/scheduler/scheduler.component';
import { ManagerRoutesComponent } from './components/manager-routes/manager-routes.component';
import { GeneretedScheduleComponent } from './components/genereted-schedule/genereted-schedule.component';
import { HistorialComponent } from './components/historial/historial.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        SidebarComponent,
        ConfigComponent,
        FilterPipe,
        SchedulerComponent,
        ManagerRoutesComponent,
        GeneretedScheduleComponent,
        HistorialComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
