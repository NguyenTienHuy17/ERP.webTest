import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { PhuongXasComponent } from './phuongXas/phuongXas/phuongXas.component';
import { NhanSusComponent } from './nhanSus/nhanSus/nhanSus.component';
import { ThanhPhosComponent } from './thanhPhos/thanhPhos/thanhPhos.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'phuongXas/phuongXas', component: PhuongXasComponent, data: { permission: 'Pages.PhuongXas' }  },
                    { path: 'nhanSus/nhanSus', component: NhanSusComponent, data: { permission: 'Pages.NhanSus' }  },
                    { path: 'thanhPhos/thanhPhos', component: ThanhPhosComponent, data: { permission: 'Pages.ThanhPhos' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
