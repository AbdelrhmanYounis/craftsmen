import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CountryService } from '../_services/country.service';
import { Governorate } from '../_models/governorate';
import { GovernorateService } from '../_services/governorate.service';
import { CityService } from '../_services/city.service';
import { Country } from '../_models/country';
import { City } from '../_models/city';
import { Craft } from '../_models/craft';
import { CraftService } from '../_services/craft.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  maxDate: Date;
  validationErrors: string[] = [];
  Countries:Country[]=[];
  Governorates:Governorate[]=[];
  Cities:City[]=[];
  Crafts:Craft[]=[];

  constructor(private accountService: AccountService, private toastr: ToastrService, 
    private fb: FormBuilder, private router: Router,private countryService:CountryService,
    private governorateService:GovernorateService,private cityService:CityService,
    private craftService:CraftService) { }

  ngOnInit(): void {
    this.intitializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
   this.getCrafts();
   this.getCountries();
  }

  intitializeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required], 
      craftId: [''],     
      country: ['', Validators.required],
      governorate: ['', Validators.required],
      cityId: ['', Validators.required],
      password: ['', [Validators.required, 
        Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value 
        ? null : {isMatching: true}
    }
  }

  register() {
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/members');
    }, error => {
      this.validationErrors = error;
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  getCrafts(){
  this.craftService.GetAll().subscribe(
    (data:Craft[])=>{
      this.Crafts=data;
    })
}
getCountries(){
  this.countryService.GetAll().subscribe(
    (data:Country[])=>{
      this.Countries=data;
    })
}

loadGovernorates(){
  this.governorateService.GetByParentId(this.registerForm.get('country').value).subscribe(
    (data:Governorate[])=>{
      this.Governorates=data;
    }
  );
}

loadCities(){
  this.cityService.GetByParentId(this.registerForm.get('governorate').value).subscribe(
    (data:City[])=>{
      this.Cities=data;
    })
    
}
}
  


