export class ConnectionString {
    value: string;
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
    connectionString: ConnectionString;
    registrationFields: RegistrationFields;
    emailSettings: EmailSettings;
}