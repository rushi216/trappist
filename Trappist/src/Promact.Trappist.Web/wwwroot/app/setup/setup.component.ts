import { Component } from '@angular/core';
import { ConnectionString } from "./setup.model";
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
    private errorMessage: boolean;
    private basicSetup: BasicSetup = new BasicSetup();
    private confirmPasswordValid: boolean;

    constructor(private setupService: SetupService) {
    }

    /**
    This method used for validating connection string
    **/
    validateConnectionString(setup: any, connectionStringName: string) {
        this.basicSetup.connectionString = new ConnectionString();
        this.basicSetup.connectionString.value = connectionStringName;
        this.setupService.validateConnectionString('api/setup/connectionstring', this.basicSetup).subscribe((tempResponse: ServiceResponse) => {
            if (tempResponse.response == true) {
                setup.next();
                this.errorMessage = false;
            }
            else {
                this.errorMessage = true;
            }
        });
    }

    /**
    This method used for verifying email Settings
    **/
    validateEmailSettings(setup: any, server: string, port: number, username: string, password: string) {
        this.basicSetup.emailSettings = new EmailSettings();
        this.basicSetup.emailSettings.server = server;
        this.basicSetup.emailSettings.port = port;
        this.basicSetup.emailSettings.userName = username;
        this.basicSetup.emailSettings.password = password;
        this.setupService.validateEmailSettings('api/setup/mailsettings', this.basicSetup).subscribe((response: EmailResponse) => {
            if (response.isMailSent == true) {
                setup.next();
                this.errorMessage = false;
            }
            else {
                this.errorMessage = true;
            }
        });
    }

    /**
    This method used for validating Password and Confirm Password matched or not.
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
    This method used for Creating user
    **/
    createUser(setup: any, name: string, email: string, password: string, confirmPassword: string) {
        this.basicSetup.registrationFields = new RegistrationFields();
        this.basicSetup.registrationFields.name = name;
        this.basicSetup.registrationFields.email = email;
        this.basicSetup.registrationFields.password = password;
        this.basicSetup.registrationFields.confirmPassword = confirmPassword;
        this.setupService.createUser('api/setup/validateuser', this.basicSetup).subscribe((tempResponse: ServiceResponse) => {
            if (tempResponse.response == true) {
                setup.complete();
                this.navigateToLogin();
                this.errorMessage = false;
            }
            else {
                this.errorMessage = true;
            }
        });
    }

    navigateToLogin() {
        window.location.href = '/login';
    }

    previousStep1(setup: any) {
        setup.previous();
    }

    previousStep2(setup: any) {
        setup.previous();
    }
}