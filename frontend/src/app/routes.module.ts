import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { globalRoutes } from './modules/global/global.routing';
import { doctorRoutes } from './modules/doctor/doctor.routing';
import { admissionRoutes } from './modules/admission/admission.routing';
import { patientRoutes } from './modules/patient/patient.routing';
import { dischargeRoutes } from './modules/discharge/discharge.routing';

@NgModule({
  imports: [
    RouterModule.forChild([
      ...globalRoutes,
      ...doctorRoutes,
      ...admissionRoutes,
      ...patientRoutes,
      ...dischargeRoutes
    ]),
  ],
  exports: [RouterModule],
})
export class RoutesModule {}
