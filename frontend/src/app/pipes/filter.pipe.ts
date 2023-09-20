import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'filter'
})
export class FilterPipe implements PipeTransform {

    transform(items: any[] | null, filterFn: (item: any) => boolean): any[] {
        if (!items) return [];
        if (!filterFn) return items;

        return items.filter(item => filterFn(item));
    }

}
