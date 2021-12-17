import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { PhuongXasServiceProxy, PhuongXaDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPhuongXaModalComponent } from './create-or-edit-phuongXa-modal.component';

import { ViewPhuongXaModalComponent } from './view-phuongXa-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';


@Component({
    templateUrl: './phuongXas.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PhuongXasComponent extends AppComponentBase {
    
    
    @ViewChild('createOrEditPhuongXaModal', { static: true }) createOrEditPhuongXaModal: CreateOrEditPhuongXaModalComponent;
    @ViewChild('viewPhuongXaModalComponent', { static: true }) viewPhuongXaModal: ViewPhuongXaModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maPhuongFilter = '';
    tenPhuongFilter = '';
    maxSoDanFilter : number;
		maxSoDanFilterEmpty : number;
		minSoDanFilter : number;
		minSoDanFilterEmpty : number;
    chuTichPhuongFilter = '';
        thanhPhoMaTPFilter = '';






    constructor(
        injector: Injector,
        private _phuongXasServiceProxy: PhuongXasServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getPhuongXas(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._phuongXasServiceProxy.getAll(
            this.filterText,
            this.maPhuongFilter,
            this.tenPhuongFilter,
            this.maxSoDanFilter == null ? this.maxSoDanFilterEmpty: this.maxSoDanFilter,
            this.minSoDanFilter == null ? this.minSoDanFilterEmpty: this.minSoDanFilter,
            this.chuTichPhuongFilter,
            this.thanhPhoMaTPFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createPhuongXa(): void {
        this.createOrEditPhuongXaModal.show();        
    }


    deletePhuongXa(phuongXa: PhuongXaDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._phuongXasServiceProxy.delete(phuongXa.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._phuongXasServiceProxy.getPhuongXasToExcel(
        this.filterText,
            this.maPhuongFilter,
            this.tenPhuongFilter,
            this.maxSoDanFilter == null ? this.maxSoDanFilterEmpty: this.maxSoDanFilter,
            this.minSoDanFilter == null ? this.minSoDanFilterEmpty: this.minSoDanFilter,
            this.chuTichPhuongFilter,
            this.thanhPhoMaTPFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    
    
    
    
    
}
