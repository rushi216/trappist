import { Component } from '@angular/core';
import { ConnectionStringParameter } from "./setup.model";
import { SetupService } from "./setup.service";
import { Http } from "@angular/http";
import { ServiceResponse } from "./setup.model";
import { EmailResponse } from "./setup.model";
import { EmailSettings } from "./setup.model";
import { BasicSetup } from "./setup.model";
@Component({
  moduleId: module.id,
  selector: 'setup',
  templateUrl: 'setup.html',
})

export class SetupComponent {

    private connectionString: ConnectionStringParameter = new ConnectionStringParameter();
    private errorMessage: boolean;
    private isValid: boolean;
    private emailSettings: EmailSettings = new EmailSettings();
    private basicSetup: BasicSetup = new BasicSetup();
    constructor(private setupService: SetupService) {
    }

    validateConnectionString(connectionStringName: string) {
        this.connectionString.ConnectionString = connectionStringName;
        this.setupService.validateConnectionString('api/BasicSetup/connection', this.connectionString).subscribe((response: ServiceResponse) => {
            if (response.Response == true) {
                this.isValid = true;
                this.errorMessage = false;
            }
            else {
                this.isValid = false;
                this.errorMessage = true;
            }
        });
    }
    validateEmailSettings(server: string, port: number, username: string, password: string) {
        this.emailSettings.Server = server;
        this.emailSettings.Port = port;
        this.emailSettings.UserName = username;
        this.emailSettings.Password = password;
        this.setupService.validateEmailSettings('api/BasicSetup/mail', this.emailSettings).subscribe((response: EmailResponse) => {
            if (response.IsMailSent == true) {
                this.isValid = true;
                this.errorMessage = false;
            }
            else {
                this.isValid = false;
                this.errorMessage = true;
            }
        });
    }
    createUser(name: string, email: string, password: string, confirmPassword: string) {
        this.basicSetup.Name = name;
        this.basicSetup.Email = email;
        this.basicSetup.Password = password;
        this.basicSetup.ConfirmPassword = confirmPassword;
        this.setupService.createUser('api/BasicSetup/validateuser', this.basicSetup).subscribe((response: ServiceResponse) => {
            if (response.Response == true) {
                this.isValid = true;
                this.errorMessage = false;
            }
            else {
                this.isValid = false;
                this.errorMessage = true;
            }
        });
    }

  navigateToLogin() {
    window.location.href = '/login';
  }

  //Wizard Step Events
  nextStep2(setup: any) {
    setup.next();
  }

  nextStep3(setup: any) {
    setup.next();
  }

  previousStep1(setup: any) {
    setup.previous();
  }

  previousStep2(setup: any) {
    setup.previous();
  }

  finish(setup: any) {
    setup.complete();
  }
}