import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ThanhPhosComponent } from './thanhPhos/thanhPhos/thanhPhos.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
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
