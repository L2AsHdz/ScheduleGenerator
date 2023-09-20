import { Component, OnInit } from '@angular/core';
import { Observable, filter, map } from 'rxjs';
import { Parametro, TipoParametro } from 'src/app/models/parametro';
import { ParametroService } from 'src/app/services/parametro.service';
import Swal from 'sweetalert2'

@Component({
    selector: 'app-config',
    templateUrl: './config.component.html',
    styleUrls: ['./config.component.css']
})
export class ConfigComponent implements OnInit {

    parametros: Observable<Parametro[]>;

    constructor(private parametroService: ParametroService) { }

    ngOnInit(): void {
        this.parametros = this.parametroService.getParametros();
    }

    configParams = (param: Parametro) => param.tipo == 0;
    priorityParams = (param: Parametro) => param.tipo == 1;
    behaviorParams = (param: Parametro) => param.tipo == 2;

    switchState(param: Parametro) {
        param.valor = param.valor == "1" ? "0" : "1";
        this.parametroService.updateParametro(param)
            .subscribe();
    }

    async changeValue(param: Parametro) {
        const { value: newValue } = await Swal.fire({
            title: 'Ingrese el  nuevo valor',
            input: 'text',
            inputLabel: `Valor actual: ${param.valor}`,
            showCancelButton: true
        });

        if (!newValue) {
            Swal.fire('Error', 'El nuevo valor no puede estar vacío', 'error');
        };

        param.valor = newValue;
        this.parametroService.updateParametro(param)
            .subscribe(() => {
                Swal.fire('Valor actualizado', `El valor de ${param.nombre} ha sido actualizado`, 'success');
            });
    }
}
