import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetAll(controllerName:string){
    return  this.http.get(this.baseUrl+controllerName);
      
    }
    GetByParentId(controllerName:string,id:number){
      return this.http.get(this.baseUrl+controllerName+'/GetByParentId/'+id);
      /*.pipe(
      map( (data:any)=>{ 
         console.log("3e3r3"+data);
         return data;
       }));
       */
     }

   GetById(controllerName:string,id:number){

    return this.http.get(this.baseUrl+controllerName+'/GetById/'+id).subscribe(
      (data:any)=>{        
        return data;
      });
   }

   Create(controllerName:string,model: any) {

    return this.http.post(this.baseUrl+controllerName+'/Create',model).subscribe(
      (data:any)=>{        
        return data;
      });
}

Update(controllerName:string,model: any) {

    return this.http.put(this.baseUrl+controllerName+'/Update',model).subscribe(
      (data:any)=>{        
        return data;
      });
}

Delete(controllerName:string,id: number) {

  return this.http.delete(this.baseUrl+controllerName+'/Delete/'+id).subscribe(
    (data:any)=>{        
      return data;
    });}

}
