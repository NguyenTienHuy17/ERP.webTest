import {AppConsts} from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { NhanSusServiceProxy, NhanSuDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditNhanSuModalComponent } from './create-or-edit-nhanSu-modal.component';

import { ViewNhanSuModalComponent } from './view-nhanSu-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';


@Component({
    templateUrl: './nhanSus.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class NhanSusComponent extends AppComponentBase {
    
    
    @ViewChild('createOrEditNhanSuModal', { static: true }) createOrEditNhanSuModal: CreateOrEditNhanSuModalComponent;
    @ViewChild('viewNhanSuModalComponent', { static: true }) viewNhanSuModal: ViewNhanSuModalComponent;   
    
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maNhanSuFilter = '';
    tenNhanSuFilter = '';
    phongBanFilter = '';
    queQuanFilter = '';
    maxThamNienFilter : number;
		maxThamNienFilterEmpty : number;
		minThamNienFilter : number;
		minThamNienFilterEmpty : number;
    maxTuoiFilter : number;
		maxTuoiFilterEmpty : number;
		minTuoiFilter : number;
		minTuoiFilterEmpty : number;






    constructor(
        injector: Injector,
        private _nhanSusServiceProxy: NhanSusServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getNhanSus(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._nhanSusServiceProxy.getAll(
            this.filterText,
            this.maNhanSuFilter,
            this.tenNhanSuFilter,
            this.phongBanFilter,
            this.maxThamNienFilter == null ? this.maxThamNienFilterEmpty: this.maxThamNienFilter,
            this.minThamNienFilter == null ? this.minThamNienFilterEmpty: this.minThamNienFilter,
            this.maxTuoiFilter == null ? this.maxTuoiFilterEmpty: this.maxTuoiFilter,
            this.minTuoiFilter == null ? this.minTuoiFilterEmpty: this.minTuoiFilter,
            this.queQuanFilter,
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

    createNhanSu(): void {
        this.createOrEditNhanSuModal.show();        
    }


    deleteNhanSu(nhanSu: NhanSuDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._nhanSusServiceProxy.delete(nhanSu.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._nhanSusServiceProxy.getNhanSusToExcel(
        this.filterText,
            this.maNhanSuFilter,
            this.tenNhanSuFilter,
            this.phongBanFilter,
            this.maxThamNienFilter == null ? this.maxThamNienFilterEmpty: this.maxThamNienFilter,
            this.minThamNienFilter == null ? this.minThamNienFilterEmpty: this.minThamNienFilter,
            this.maxTuoiFilter == null ? this.maxTuoiFilterEmpty: this.maxTuoiFilter,
            this.minTuoiFilter == null ? this.minTuoiFilterEmpty: this.minTuoiFilter,
            this.queQuanFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    
    
    
    
    
}
