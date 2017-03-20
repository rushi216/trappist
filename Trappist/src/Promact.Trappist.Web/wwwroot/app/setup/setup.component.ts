import { Component } from '@angular/core';
import { ConnectionStringParamters } from "./setup.model";
import { SetupService } from "./setup.service";
import { ServiceResponse } from "./setup.model";
import { EmailResponse } from "./setup.model";
import { EmailSettings } from "./setup.model";
import { BasicSetup } from "./setup.model";
import { RegistrationFields } from "./setup.model";
@Component({
  moduleId: module.id,
  selector: 'setup',
  templateUrl: 'setup.html',
})

export class SetupComponent {
    private connectionString: ConnectionStringParamters = new ConnectionStringParamters();
    private errorMessage: boolean;
    private isValid: boolean;
    private isValidSecondStep: boolean;
    private isValidThirdStep: boolean;
    private emailSettings: EmailSettings = new EmailSettings();
    private basicSetup: BasicSetup = new BasicSetup();
    private confirmPasswordValid: boolean;
    constructor(private setupService: SetupService) {
    }
    /**
    Method for validate connection string
    **/
    validateConnectionString(connectionStringName: string) {
        this.basicSetup.connectionStringParameters = new ConnectionStringParamters();
        this.basicSetup.connectionStringParameters.connectionString = connectionStringName;
        this.setupService.validateConnectionString('api/BasicSetup/ConnectionString', this.basicSetup).subscribe((tempResponse: ServiceResponse) => {
            if (tempResponse.response == true) {
                this.isValid = true;
                this.errorMessage = false;
            }
            else {
                this.isValid = false;
                this.errorMessage = true;
            }
        });
    }
    /**
    Method for validate email Settings
    **/
    validateEmailSettings(server: string, port: number, username: string, password: string) {
        this.basicSetup.emailSettings = new EmailSettings();
        this.basicSetup.emailSettings.server = server;
        this.basicSetup.emailSettings.port = port;
        this.basicSetup.emailSettings.userName = username;
        this.basicSetup.emailSettings.password = password;
        this.setupService.validateEmailSettings('api/BasicSetup/MailSettings', this.basicSetup).subscribe((response: EmailResponse) => {
            if (response.isMailSent == true) {
                this.isValidSecondStep = true;
                this.errorMessage = false;
            }
            else {
                this.isValidSecondStep = true;
                this.errorMessage = true;
            }
        });
    }
    /**
    Method for validate Password and Confirm Password matched or not.
    **/
    isValidPassword(password: string, confirmPassword: string) {
        if (confirmPassword == password) {
            this.confirmPasswordValid = true;
        }
        else {
            this.confirmPasswordValid = false;
        }
    }
    /**
    Method for Creating user
    **/
    createUser(name: string, email: string, password: string, confirmPassword: string) {
        this.basicSetup.registrationFields = new RegistrationFields();
        this.basicSetup.registrationFields.name = name;
        this.basicSetup.registrationFields.email = email;
        this.basicSetup.registrationFields.password = password;
        this.basicSetup.registrationFields.confirmPassword = confirmPassword;
        this.setupService.createUser('api/BasicSetup/Validateuser', this.basicSetup).subscribe((tempResponse: ServiceResponse) => {
            if (tempResponse.response == true) {
                this.isValidThirdStep = true;
                this.errorMessage = false;
            }
            else {
                this.isValidThirdStep = false;
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