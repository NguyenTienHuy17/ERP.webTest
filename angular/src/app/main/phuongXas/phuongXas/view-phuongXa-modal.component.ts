import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPhuongXaForViewDto, PhuongXaDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPhuongXaModal',
    templateUrl: './view-phuongXa-modal.component.html'
})
export class ViewPhuongXaModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPhuongXaForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPhuongXaForViewDto();
        this.item.phuongXa = new PhuongXaDto();
    }

    show(item: GetPhuongXaForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
