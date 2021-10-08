import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ThanhPhosServiceProxy, CreateOrEditThanhPhoDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';




@Component({
    selector: 'createOrEditThanhPhoModal',
    templateUrl: './create-or-edit-thanhPho-modal.component.html'
})
export class CreateOrEditThanhPhoModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    thanhPho: CreateOrEditThanhPhoDto = new CreateOrEditThanhPhoDto();




    constructor(
        injector: Injector,
        private _thanhPhosServiceProxy: ThanhPhosServiceProxy
    ) {
        super(injector);
    }
    
    show(thanhPhoId?: number): void {
    

        if (!thanhPhoId) {
            this.thanhPho = new CreateOrEditThanhPhoDto();
            this.thanhPho.id = thanhPhoId;


            this.active = true;
            this.modal.show();
        } else {
            this._thanhPhosServiceProxy.getThanhPhoForEdit(thanhPhoId).subscribe(result => {
                this.thanhPho = result.thanhPho;



                this.active = true;
                this.modal.show();
            });
        }
        
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._thanhPhosServiceProxy.createOrEdit(this.thanhPho)
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
