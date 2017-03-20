export class ConnectionStringParamters {
    connectionString: string;
}

export class ServiceResponse {
    response: boolean;
}

export class EmailSettings {
    server: string;
    port: number;
    userName: string;
    password: string;
}

export class EmailResponse {
    isMailSent: boolean;
}

export class RegistrationFields {
    name: string;
    email: string;
    password: string;
    confirmPassword: string;
}
export class BasicSetup {    
    connectionStringParameters: ConnectionStringParamters;
    registrationFields: RegistrationFields;
    emailSettings: EmailSettings;
}