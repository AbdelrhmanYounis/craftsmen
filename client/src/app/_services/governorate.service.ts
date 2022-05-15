import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Governorate } from '../_models/governorate';
import { RepositoryService } from './repository.service';

@Injectable({
  providedIn: 'root'
})
export class GovernorateService {

  serviceName="Governorate";

  constructor(private Repository:RepositoryService) { 
  }

  GetAll(){
   return this.Repository.GetAll(this.serviceName);
   }
   GetByParentId(countryId:number){
    return this.Repository.GetByParentId(this.serviceName,countryId)
    
    }

   GetById(id:number){
   return this.Repository.GetById(this.serviceName,id);   
   }

   Create(model: Governorate) {
  return this.Repository.Create(this.serviceName,model);
}

Update(model: Governorate) {
  return this.Repository.Update(this.serviceName,model);
}

Delete(id: number) {
  return this.Repository.Delete(this.serviceName,id);
}
}
