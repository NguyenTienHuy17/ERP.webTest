import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PhuongXasServiceProxy, CreateOrEditPhuongXaDto ,PhuongXaThanhPhoLookupTableDto
					} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';




@Component({
    selector: 'createOrEditPhuongXaModal',
    templateUrl: './create-or-edit-phuongXa-modal.component.html'
})
export class CreateOrEditPhuongXaModalComponent extends AppComponentBase implements OnInit{
   
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    phuongXa: CreateOrEditPhuongXaDto = new CreateOrEditPhuongXaDto();

    thanhPhoMaTP = '';

	allThanhPhos: PhuongXaThanhPhoLookupTableDto[];
					

    constructor(
        injector: Injector,
        private _phuongXasServiceProxy: PhuongXasServiceProxy
    ) {
        super(injector);
    }
    
    show(phuongXaId?: number): void {
    

        if (!phuongXaId) {
            this.phuongXa = new CreateOrEditPhuongXaDto();
            this.phuongXa.id = phuongXaId;
            this.thanhPhoMaTP = '';


            this.active = true;
            this.modal.show();
        } else {
            this._phuongXasServiceProxy.getPhuongXaForEdit(phuongXaId).subscribe(result => {
                this.phuongXa = result.phuongXa;

                this.thanhPhoMaTP = result.thanhPhoMaTP;


                this.active = true;
                this.modal.show();
            });
        }
        this._phuongXasServiceProxy.getAllThanhPhoForTableDropdown().subscribe(result => {						
						this.allThanhPhos = result;
					});
					
        
    }

    save(): void {
            this.saving = true;
            
			
			
            this._phuongXasServiceProxy.createOrEdit(this.phuongXa)
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
