import { Component, EventEmitter, Input, Output, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-select-input',
  templateUrl: './select-input.component.html',
  styleUrls: ['./select-input.component.css','../../home/home.component.scss']
})
export class SelectInputComponent implements  ControlValueAccessor{
  @Input() label: string;
  @Input() type = 'Country';
  @Input() items :any[];

  
  constructor(@Self() public ngControl: NgControl) { 
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }

 
}
