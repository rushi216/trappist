export class ConnectionStringParameter {
    ConnectionString: string;
}

export class ServiceResponse {
    Response: boolean;
}

export class EmailSettings {
    Server: string;
    Port: number;
    UserName: string;
    Password: string;
}

export class EmailResponse {
    IsMailSent: boolean;
}

export class BasicSetup {
    Name: string;
    Email: string;
    Password: string;
    ConfirmPassword: string;
}