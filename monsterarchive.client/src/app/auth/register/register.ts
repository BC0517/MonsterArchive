import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  form!: UntypedFormGroup;
  constructor(private authService: AuthService, private router: Router) {}
  ngOnInit(): void {
    // Initialize the form here
    this.form = new UntypedFormGroup({
      // Define form controls here
      username: new FormControl('',Validators.required),
      password: new FormControl('',Validators.required),
      email: new FormControl('',Validators.required)
    });
  }
  onSubmit(){
    let registerRequest = {
      username: this.form.controls['username'].value,
      password: this.form.controls['password'].value,
      email: this.form.controls['email'].value
    }
    this.authService.register(registerRequest).subscribe({
      next: result => {
        console.log("Registration successful", result);
        this.router.navigate(['/login']);
      },
      error: error => {
        console.log("Registration failed", error);
      }
    });
  }
}
