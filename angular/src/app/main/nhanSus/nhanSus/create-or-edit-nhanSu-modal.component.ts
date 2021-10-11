import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { NhanSusServiceProxy, CreateOrEditNhanSuDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';




@Component({
    selector: 'createOrEditNhanSuModal',
    templateUrl: './create-or-edit-nhanSu-modal.component.html'
})
export class CreateOrEditNhanSuModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    nhanSu: CreateOrEditNhanSuDto = new CreateOrEditNhanSuDto();




    constructor(
        injector: Injector,
        private _nhanSusServiceProxy: NhanSusServiceProxy
    ) {
        super(injector);
    }
    
    show(nhanSuId?: number): void {
    

        if (!nhanSuId) {
            this.nhanSu = new CreateOrEditNhanSuDto();
            this.nhanSu.id = nhanSuId;


            this.active = true;
            this.modal.show();
        } else {
            this._nhanSusServiceProxy.getNhanSuForEdit(nhanSuId).subscribe(result => {
                this.nhanSu = result.nhanSu;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._nhanSusServiceProxy.createOrEdit(this.nhanSu)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }













    close(): void {
        this.active = false;
        this.modal.hide();
    }
    
     ngOnInit(): void {
        
     }    
}
