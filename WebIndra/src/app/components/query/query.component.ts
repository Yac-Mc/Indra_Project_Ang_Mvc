import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-query',
  templateUrl: './query.component.html',
  styleUrls: ['./query.component.css']
})
export class QueryComponent implements OnInit {

  queryForm: FormGroup;
  flagResult = false;
  submitted = false;
  resultQuery: any;

  constructor(private formBuilder: FormBuilder, private userService: UsersService) { }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm(){
    this.queryForm = this.formBuilder.group({
      UserName: ['', [Validators.pattern('^[A-Za-z0-9\\s]+$'), Validators.maxLength(100), this._validateSpaces.bind(this)]],
      ResMin: [Number],
      ResMax: [Number]
    });
  }

  // Obtención de campos para facil control del formulario
  get f() { return this.queryForm.controls; }

  _validateSpaces(c: AbstractControl): object{
    const inputtValue: string = c.value.toString();
    if (inputtValue.trim().length > 0){
      return null;
    }else{return { spaces: {valid: false } }; }
  }

  onSubmit() {
    this.flagResult = false;
    this.submitted = true;
    if (this.queryForm.invalid) {
      return;
    }
    const queryFilter: any = {
      UserName: String(this.queryForm.controls[`UserName`].value),
    };
    
    let number = this.queryForm.controls[`ResMin`].value;
    if (Number.isInteger(number)){
      queryFilter.ResponseMin = Number(number);
    }else{
      queryFilter.ResponseMin = Number(0);
    }

    number = this.queryForm.controls[`ResMax`].value;
    if (Number.isInteger(number)){
      queryFilter.ResponseMax = Number(number);
    }else{
      queryFilter.ResponseMax = Number(0);
    }

    this.userService.getUsersFilter(queryFilter).subscribe(result => {
      this.resultQuery = result;
      this.flagResult = true;
      console.log(this.resultQuery);
    });
  }

  deleteRegister(id: number){
    this.userService.deleteRegister(id).subscribe(result => {
      this.resultQuery = result;
      this.flagResult = true;
      this.onSubmit();
    });
  }

}
