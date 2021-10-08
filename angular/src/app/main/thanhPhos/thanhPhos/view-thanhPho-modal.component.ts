import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetThanhPhoForViewDto, ThanhPhoDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewThanhPhoModal',
    templateUrl: './view-thanhPho-modal.component.html'
})
export class ViewThanhPhoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetThanhPhoForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetThanhPhoForViewDto();
        this.item.thanhPho = new ThanhPhoDto();
    }

    show(item: GetThanhPhoForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
