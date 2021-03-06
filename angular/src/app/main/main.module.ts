import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { PhuongXasComponent } from './phuongXas/phuongXas/phuongXas.component';
import { ViewPhuongXaModalComponent } from './phuongXas/phuongXas/view-phuongXa-modal.component';
import { CreateOrEditPhuongXaModalComponent } from './phuongXas/phuongXas/create-or-edit-phuongXa-modal.component';

import { NhanSusComponent } from './nhanSus/nhanSus/nhanSus.component';
import { ViewNhanSuModalComponent } from './nhanSus/nhanSus/view-nhanSu-modal.component';
import { CreateOrEditNhanSuModalComponent } from './nhanSus/nhanSus/create-or-edit-nhanSu-modal.component';

import { ThanhPhosComponent } from './thanhPhos/thanhPhos/thanhPhos.component';
import { ViewThanhPhoModalComponent } from './thanhPhos/thanhPhos/view-thanhPho-modal.component';
import { CreateOrEditThanhPhoModalComponent } from './thanhPhos/thanhPhos/create-or-edit-thanhPho-modal.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask';import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
		FileUploadModule,
		AutoCompleteModule,
		PaginatorModule,
		EditorModule,
		InputMaskModule,		TableModule,

        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot()
    ],
    declarations: [
		PhuongXasComponent,

		ViewPhuongXaModalComponent,
		CreateOrEditPhuongXaModalComponent,
		NhanSusComponent,

		ViewNhanSuModalComponent,
		CreateOrEditNhanSuModalComponent,
		ThanhPhosComponent,

		ViewThanhPhoModalComponent,
		CreateOrEditThanhPhoModalComponent,
        DashboardComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
