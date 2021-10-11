import {AppConsts} from "@shared/AppConsts";
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetNhanSuForViewDto, NhanSuDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewNhanSuModal',
    templateUrl: './view-nhanSu-modal.component.html'
})
export class ViewNhanSuModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetNhanSuForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetNhanSuForViewDto();
        this.item.nhanSu = new NhanSuDto();
    }

    show(item: GetNhanSuForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }
    
    

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
